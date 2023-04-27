namespace CarFleetManagement_API.DTOs
{
    public class VehicleDTO
    {
        public long VehicleID { get; set; } 
        public string? RegistrationNo { get; set; }
        public string? Brand { get; set;}
        public string? Model { get; set;}
        public string? Photo { get; set;}
        public DateTime FirstInspectionDate { get; set;}
        public DateTime CurrentInspectionDate { get; set;}
        public bool IsRoadWorthy { get; set;}
        public DateTime NextInspectionDate { get; set; }
        public bool IsInspectionExpired { get; set; }
    }
}
