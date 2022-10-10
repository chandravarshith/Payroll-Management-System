using System.ComponentModel.DataAnnotations;

namespace Payroll_Management_System.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Employee> DeptEmployees { get; set; }
    }
}
