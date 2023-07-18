using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll_Management_System.Models
{
    public class Leave
    {
        [DisplayName("Leave Id")]
        public int Id { get; set; }

        [DisplayName("Employee Id")]
        public int EmployeeId { get; set; }

        [DisplayName("Leave Status")]
        public string LeaveStatus { get; set; }

        //implement as date need work through google

        [DisplayName("From (Date)")]
        public string Date { get; set; }

        [DisplayName("Number of Days")]
        [Range(1, 10, ErrorMessage = "Please a enter a value only in between 1 to 10!")]
        public int NumOfDays { get; set; }

        public string Reason { get; set; }

        public string? EmployeeName { get; set; }
    }
}
