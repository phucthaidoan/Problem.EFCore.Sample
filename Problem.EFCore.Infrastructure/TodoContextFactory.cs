using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Problem.EFCore.Infrastructure.Data;

namespace Problem.EFCore.Infrastructure
{
    public class TodoContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-IAJ1J0A2;Database=todo_01simple;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");

            return new TodoDbContext(optionsBuilder.Options);
        }
    }
}
