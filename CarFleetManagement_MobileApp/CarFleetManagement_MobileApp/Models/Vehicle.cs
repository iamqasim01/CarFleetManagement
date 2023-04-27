using System;
using System.Collections.Generic;
using System.Text;

namespace CarFleetManagement_MobileApp.Models
{
    public class Vehicle
    {
        public long VehicleID { get; set; }
        public string RegistrationNo { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Photo { get; set; }
    }
}
