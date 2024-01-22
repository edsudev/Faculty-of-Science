namespace Faculty_Portal.Models
{
    public class FacultyStaff
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public string? Slug { get; set; }
        public string? Position { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
