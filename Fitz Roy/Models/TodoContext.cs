using Microsoft.EntityFrameworkCore;

namespace Fitz_Roy.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }
        public DbSet<TareaItem> Tarea { get; set; }
    }
}
