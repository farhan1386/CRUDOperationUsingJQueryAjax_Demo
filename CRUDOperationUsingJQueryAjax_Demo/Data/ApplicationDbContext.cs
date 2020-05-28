using CRUDOperationUsingJQueryAjax_Demo.Models;
using System.Data.Entity;

namespace CRUDOperationUsingJQueryAjax_Demo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("StudentDB")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
    }
}