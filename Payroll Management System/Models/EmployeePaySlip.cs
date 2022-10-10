namespace Payroll_Management_System.Models
{
    public class EmployeePaySlip
    {
        public Employee Emp { get; set; }

        public Level EmpLevel { get; set; }

        public Payslip PaySlip { get; set; }
    }
}
