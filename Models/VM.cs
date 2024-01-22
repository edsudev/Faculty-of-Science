namespace Faculty_Portal.Models
{
    public class VM
    {
        public List<Dean>? Deans { get; set; }
        public List<HOD>? HODs { get; set; }
        public List<FacultyStaff>? FacultyStaffs { get; set; }
        public List<DepartmentStaff>? DepartmentStaffs { get; set; }
        public List<Department>? Departments { get; set; }
        public Subscription? Subscriptions { get; set; }
    }
}
