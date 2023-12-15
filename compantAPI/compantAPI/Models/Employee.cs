using System.ComponentModel.DataAnnotations;

namespace compantAPI.Models
{
    public class Employee
    {
       
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentPart { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string PhotoFileName { get; set; }
        public string EmpPassword { get; set; }
        public string EmpCPassword { get; set; }
        public string GenderType { get; set; }
    }
}
