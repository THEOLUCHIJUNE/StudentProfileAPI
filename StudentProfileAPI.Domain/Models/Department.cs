namespace StudentProfileAPI.Domain.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int FacultyId { get; set; } // Foreign key
        public Faculty Faculty { get; set; } // Navigation property
        public ICollection<Student> Students { get; set; } // Navigation property
    }
}