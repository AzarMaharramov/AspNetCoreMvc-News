namespace MyFirstProject.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool Status { get; set; }
    }
}
