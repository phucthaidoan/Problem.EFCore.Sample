using Microsoft.EntityFrameworkCore;
using Problem.EFCore.Sample.Data.Entities;

namespace Problem.EFCore.Sample.Data
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
