namespace MyFirstProject.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; } 
        public DateTime Date { get; set; }
        public string? Image { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
