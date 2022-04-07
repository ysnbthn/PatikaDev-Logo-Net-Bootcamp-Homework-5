using Domain.Entities;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace HW5.Producer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private HttpClient httpClient;
        private ConnectionFactory factory;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            httpClient = new HttpClient();

            factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                "post-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var request = await httpClient.GetAsync("https://jsonplaceholder.typicode.com/posts");
                if (request.IsSuccessStatusCode)
                {
                    string responseBody = await request.Content.ReadAsStringAsync();
                    List<Post> posts = JsonConvert.DeserializeObject<List<Post>>(responseBody).ToList();

                    posts.ForEach(x =>
                    {
                        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(x));
                        channel.BasicPublish("", "post-queue", null, body);
                    });
                }
                else
                {
                    _logger.LogInformation("Error occured while getting posts.");
                }
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}