using System.ComponentModel.DataAnnotations;

namespace CRUDOperationUsingJQueryAjax_Demo.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
    }
}