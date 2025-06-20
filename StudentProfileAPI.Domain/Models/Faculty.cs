namespace StudentProfileAPI.Domain.Models
{
    public class Faculty
    {
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public ICollection<Department> Departments { get; set; } // Navigation property
    }
}