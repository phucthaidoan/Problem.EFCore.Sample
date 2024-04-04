using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Problem.EFCore.Infrastructure.Data;
using TotoFunctionApp;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        var connectionString = context.Configuration.GetSection("ConnectionString").Value;
        services.AddDbContext<TodoDbContext>((sp, options) =>
        {
            options
                .UseSqlServer(connectionString);
        });
        services.AddScoped<IPlanService, PlanService>();
    })
    .Build();

host.Run();
