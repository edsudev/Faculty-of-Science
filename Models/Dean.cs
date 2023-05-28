namespace Faculty_Portal.Models
{
    public class Dean
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Picture { get; set; }
        public bool IsActive { get; set; }
        public string? Message { get; set; }
        public DateTime ResumedOn { get; set; }
        public DateTime EndedOn { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
