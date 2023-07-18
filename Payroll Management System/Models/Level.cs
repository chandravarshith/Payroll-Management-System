using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payroll_Management_System.Models
{
    public class Level
    {
        [DisplayName("Level Id")]
        public int LevelId { get; set; }

        [DisplayName("Basic Pay")]
        public double BasicPay { get; set; }

        [DisplayName("House Rent Allowance")]
        public double HouseRentAllowance { get; set; }

        [DisplayName("Travel Allowance")]
        public double TravelAllowance { get; set; }

        [DisplayName("Medical Allowance")]
        public double MedicalAllowance { get; set; }

        [DisplayName("Tax Deductions")]
        public double TaxDeductions { get; set; }

        [DisplayName("Monthly leaves permitted")]
        public int MonthlyLeavesPermitted { get; set; }

        [DisplayName("Loss of pay")]
        public double LossOfPay { get; set; }

        public IEnumerable<Employee> LevelEmployees { get; set; }

    }
}


