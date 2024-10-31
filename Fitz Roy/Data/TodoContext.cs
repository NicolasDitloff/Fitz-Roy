using Microsoft.EntityFrameworkCore;
using Fitz_Roy.Models;

namespace Fitz_Roy.Data
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
        }

        public DbSet<TareaItem> Tareas { get; set; }
    }
}