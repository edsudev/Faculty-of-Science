using System.ComponentModel.DataAnnotations.Schema;
namespace Faculty_Portal.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string? News { get; set; }
        public string? Topic { get; set; }
        public string? Picture { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }
        public Department? Departments { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } 
    }
}
