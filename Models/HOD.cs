using System.ComponentModel.DataAnnotations.Schema;
using static Faculty_Portal.Models.Enum;
namespace Faculty_Portal.Models
{
    public class HOD
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Email { get; set; }
        public string? Picture { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public bool IsActive { get; set; }
        public string? Message { get; set; }
        public HODType? Type { get; set; }
        public DateTime ResumedOn { get; set; }
        public DateTime EndedOn { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; }
    }
}
