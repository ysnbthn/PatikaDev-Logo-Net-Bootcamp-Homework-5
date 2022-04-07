using HW5.Consumer;
using HW5.Consumer.Profiles;
using HW5.Repository.Abstract;
using HW5.Repository.Concreate;
using Microsoft.EntityFrameworkCore;
using Persistence;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddTransient<IPostRepository, PostRepository>();
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddDbContext<HW5DbContext>(opt => opt.UseSqlServer("server=(localdb)\\mssqllocaldb;database=HW5;Trusted_Connection=true"));
    })
    .Build();

await host.RunAsync();
