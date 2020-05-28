using System.ComponentModel.DataAnnotations;

namespace CRUDOperationUsingJQueryAjax_Demo.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

    }
}