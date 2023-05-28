using System.ComponentModel.DataAnnotations.Schema;
using static Faculty_Portal.Models.Enum;

namespace Faculty_Portal.Models
{
    public class DepartmentStaff
    {
        public int Id { get; set; }
        public TypeOfStaff? Type { get; set; }
        public string? Name { get; set; }
        public string? Picture { get; set; }
        public string? ResearchArea { get; set; }
        public string? Education { get; set; }
        public string? Position { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public string? Rank { get; set; }
        public bool IsActive { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }

        public string? ResearchGate { get; set; }
        public string? LinkedIn { get; set; }
        public string? GoogleScholar { get; set; }
        public string? Scopus { get; set; }
        public string? CV { get; set; }
        public string? ORCID { get; set; }
    }
}
