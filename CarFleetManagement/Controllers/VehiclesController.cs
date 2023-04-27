using CarFleetManagement_API.Data;
using CarFleetManagement_API.DTOs;
using CarFleetManagement_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace CarFleetManagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly CarFleetContext _context;
        public VehiclesController(CarFleetContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VehicleDTO>> GetVehicles() 
        {
            try
            {
                var vehicles = _context.Vehicle.Include(v => v.TechnicalTests).ToList();

                if (vehicles is null)
                {
                    return NotFound();
                }

                var vehicleDTO = Map(vehicles);
                return Ok(vehicleDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<VehicleDTO>> GetVehicles(string? search)
        {
            try
            {
                var query = _context.Vehicle.Include(v => v.TechnicalTests).AsQueryable();

                if (!string.IsNullOrEmpty(search)) 
                {
                    query = query.Where(v => 
                    v.RegistrationNo.Contains(search) ||
                    v.Brand.Contains(search) ||
                    v.Model.Contains(search)
                    );
                }

                var vehicles = query.ToList();

                if (vehicles is null)
                {
                    return NotFound();
                }

                var vehicleDTO = Map(vehicles);

                return Ok(vehicleDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult> UpdateVehicles(VehicleDTO vehicleDTO)
        {
            try
            {
                if (vehicleDTO is null) { return NotFound(); }

                Vehicle vehicles = new Vehicle
                {
                    Model = vehicleDTO.Model,
                    VehicleID = vehicleDTO.VehicleID,

                    TechnicalTests = new List<Vehicle_TechnicalTest>
                    {
                        new Vehicle_TechnicalTest
                        {
                            VehicleID = vehicleDTO.VehicleID,
                            InspectionDate = vehicleDTO.CurrentInspectionDate,
                            NextInspectionDate = vehicleDTO.NextInspectionDate,
                            IsOperational = vehicleDTO.IsRoadWorthy
                        }
                    }
                };

                var exisitingVal = (from insp in _context.Vehicle_TechnicalTest
                                    where insp.VehicleID == vehicleDTO.VehicleID && 
                                    insp.InspectionDate.Equals(vehicles.TechnicalTests.First().InspectionDate) 
                                    select insp).Count();

                if (exisitingVal > 0)
                {
                    return BadRequest($"The technical test is already exists for the date of {vehicles.TechnicalTests.First().InspectionDate.ToString("dd.MM.yyyy")}!");
                }

                _context.Vehicle_TechnicalTest.Add(vehicles.TechnicalTests.First());
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        /// <summary>
        /// Maps Vehicle objects to VehicleDTO 
        /// </summary>
        /// <param name="vehicles"></param>
        /// <returns></returns>
        static IEnumerable<VehicleDTO> Map(List<Vehicle> vehicles) 
        {
            var vehicleDTO = vehicles.Select(v => new VehicleDTO
            {
                VehicleID = v.VehicleID,
                RegistrationNo = v.RegistrationNo,
                Brand = v.Brand,
                Model = v.Model,
                Photo = v.Photo,

                FirstInspectionDate = v.TechnicalTests
                .OrderBy(tt => tt.InspectionDate)
                .FirstOrDefault()?
                .InspectionDate ?? DateTime.MinValue,

                CurrentInspectionDate = v.TechnicalTests
                .OrderByDescending(tt => tt.InspectionDate)
                .FirstOrDefault()?
                .InspectionDate ?? DateTime.MinValue,

                IsRoadWorthy = v.TechnicalTests
                .OrderByDescending(tt => tt.InspectionDate)
                .FirstOrDefault()?
                .IsOperational ?? false,

                NextInspectionDate = v.TechnicalTests
                .OrderByDescending(tt => tt.InspectionDate)
                .FirstOrDefault()?
                .NextInspectionDate ?? DateTime.MinValue,

                IsInspectionExpired = v.TechnicalTests
                .OrderByDescending(tt => tt.InspectionDate)
                .FirstOrDefault()?
                .NextInspectionDate < DateTime.Now
            });
            return vehicleDTO;
        }
    }
}
