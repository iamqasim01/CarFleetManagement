using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarFleetManagement_API.Models
{
    public class Vehicle
    {
        [Key]
        public long VehicleID { get; set; }
        public string RegistrationNo { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public List<Vehicle_TechnicalTest> TechnicalTests { get; set; }
    }
}
