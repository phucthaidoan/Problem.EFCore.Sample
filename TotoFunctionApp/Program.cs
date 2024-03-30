using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Problem.EFCore.Infrastructure.Data;
using TotoFunctionApp;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
        services.AddDbContext<TodoDbContext>((sp, options) =>
        {
            options
                .UseSqlServer("Server=LAPTOP-IAJ1J0A2;Database=todo_01simple;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        });
        services.AddScoped<IPlanService, PlanService>();
    })
    .Build();

host.Run();
