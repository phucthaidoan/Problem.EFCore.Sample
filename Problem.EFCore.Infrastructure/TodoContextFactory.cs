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
            optionsBuilder.UseSqlServer(args[0]);

            return new TodoDbContext(optionsBuilder.Options);
        }
    }
}
