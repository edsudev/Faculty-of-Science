namespace Faculty_Portal.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortCode { get; set; }
        public bool IsActive { get; set; }
        public string? Info { get; set; }
        public string? Picture { get; set; }
    }
}
