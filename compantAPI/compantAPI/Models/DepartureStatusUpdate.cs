using System.Data.SqlTypes;

namespace compantAPI.Models
{
    public class DepartureStatusUpdate
    {
        public int EmployeeId { get; set; }
        public string Status { get; set; }
        public SqlDateTime StartHoure { get; set; }
        public SqlDateTime EndHoure { get; set; }
    }
}
