namespace compantAPI.Models
{
    public class DeparturesRequest
    {
        public int EmployeeId { get; set; }
        public DateTime StartHoure { get; set; }
        public DateTime EndHoure { get; set; }
        public string Status { get; set; } = "Pending";
        public string EmployeeName { get; set; }
        public string DepartmentPart { get; set; }
    }
}
