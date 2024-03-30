using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Infrastructure.Data.Entities;
using Problem.EFCore.Sample.Data.Entities;

namespace Problem.EFCore.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Plan> Plans { get; set; }

    }
}
