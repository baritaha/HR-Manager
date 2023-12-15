namespace compantAPI.Models
{
    public class VacationRequest
    {

        public int EmployeeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Pending";
        public string EmployeeName { get; set; }
        public string DepartmentPart { get; set; }
        public string GenderType { get; set; }

    }
}
