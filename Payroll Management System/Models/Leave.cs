using System.ComponentModel;

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
        public int NumOfDays { get; set; }

        public string Reason { get; set; }
    }
}
