using System.Data.SqlTypes;

namespace compantAPI.Models
{
    public class VacationStatusUpdateRequest
    {
        public int EmployeeId { get; set; }
        public string Status { get; set; }
        public SqlDateTime StartDate { get; set; }
        public SqlDateTime EndDate { get; set; }
    }
}
