using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Sample.Data.Entities;

namespace Problem.EFCore.Sample.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LAPTOP-IAJ1J0A2;Database=todo_01simple;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
        }

        public DbSet<Todo> Todos { get; set;}
        public DbSet<Plan> Plans { get; set;}

    }
}
