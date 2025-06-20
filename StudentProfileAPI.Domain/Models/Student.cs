namespace StudentProfileAPI.Domain.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MatricNumber { get; set; }
        public string StateOfOrigin { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; } // Foreign key
        public Department Department { get; set; } // Navigation property
    }
}