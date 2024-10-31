namespace Fitz_Roy.Models
{
    public class TareaItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } // Pendiente, En progreso, Completada
        public DateTime CreatedAt { get; set; }
    }
}
