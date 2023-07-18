using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Payroll_Management_System.Models
{
    public class Payslip
    {
        [Key]
        public string Id { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        [DisplayName("Employee Id")]
        public int EmployeeId { get; set; }

        public double Bonus { get; set; }

        [DisplayName("Total Allowance")]
        public double TotalAllowance { get; set; }

        [DisplayName("Leave Deduction")]
        public double LeaveDeduction { get; set; }

        [DisplayName("Net Salary")]
        public double Salary { get; set; }
    }
}
