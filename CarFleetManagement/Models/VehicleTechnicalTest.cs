using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFleetManagement_API.Models
{
    public class Vehicle_TechnicalTest
    {
        [Key]
        public long TechnicalTestID { get; set; }
        [ForeignKey("Vehicle")]
        public long VehicleID { get; set; } 
        public DateTime InspectionDate { get; set; }
        public DateTime NextInspectionDate { get; set; }
        public bool IsOperational { get; set; }
    }
}
