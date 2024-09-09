namespace TodoAPP.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDone { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
