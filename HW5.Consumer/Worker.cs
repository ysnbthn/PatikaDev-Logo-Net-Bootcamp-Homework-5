using AutoMapper;
using Domain.Entities;
using HW5.Consumer.DTOs;
using HW5.Repository.Abstract;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace HW5.Consumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private ConnectionFactory factory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
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
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (sender, e) =>
                {
                    var body = e.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var post = JsonConvert.DeserializeObject<PostDto>(message);
                    
                    using(var scope = _scopeFactory.CreateScope())
                    {
                        var postRepo = scope.ServiceProvider.GetRequiredService<IPostRepository>();
                        var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
                        postRepo.AddPost(mapper.Map<Post>(post));
                    }

                };

                channel.BasicConsume("post-queue", autoAck: true, consumer);

                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}