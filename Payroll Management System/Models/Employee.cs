using Payroll_Management_System.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll_Management_System.Models
{
    public class Employee
    {
        [Key]
        [DisplayName("Employee Id")]
        public int EmployeeId { get; set; }

        public string UserId { get; set; }

        [DisplayName("Level Id")]
        public int LevelId { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        [DisplayName("Mail Id")]
        public string MailId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string Zipcode { get; set; }

        public string Department { get; set; }

        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("Bank Account Number")]
        public string BankAccountNumber { get; set; }

        [DisplayName("Work Location")]
        public string WorkLocation { get; set; }

        [DisplayName("Leaves Taken")]
        public int NumOfLeaves { get; set; }

        public string Status { get; set; }

        public IEnumerable<Leave> EmpLeaves { get; set; }

        public IEnumerable<Payslip> EmpPayslips { get; set; }
    }
}
