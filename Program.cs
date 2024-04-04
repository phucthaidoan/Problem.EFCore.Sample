
using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Sample.Data;
using Problem.EFCore.Sample.Events;

namespace Problem.EFCore.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetSection("DbConnectionString").Value;
            builder.Services.AddDbContext<TodoDbContext>((sp, options) =>
            {
                options
                    .UseSqlServer(connectionString);
            });
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ITodoService, TodoService>();

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<TodoToogleEventHandler>());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
