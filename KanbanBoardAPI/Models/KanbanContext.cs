using Microsoft.EntityFrameworkCore;

namespace KanbanBoardAPI.Models
{
    public class KanbanContext : DbContext
    {
        public KanbanContext(DbContextOptions<KanbanContext> options) : base(options) { }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Column> Columns { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
