using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHS.Entity;
using VHSBackend.Core.Integrations;
using VHSBackend.Core.Repository;

namespace VHSBackend.Web.Controllers
{
    [Route("api/route")]
    [ApiController]
    public class GPSController : ControllerBase
    {
        public GPSController()
        {
            _sqlStatusRepository = new SqlStatusRepository();
            _cDSUserRepository = new CDSUserRepository();
            _sqlVehicleRepository = new SqlVehicleRepository();
        }
        private readonly SqlStatusRepository _sqlStatusRepository;
        private readonly CDSUserRepository _cDSUserRepository;
        private readonly SqlVehicleRepository _sqlVehicleRepository;

        Status status = new Status();

        // Endpoint for users to send new route destination
        [HttpPost]
        [Route("{vin}/destination")]
        public ActionResult<string> SendDestinationToVehicle(string vin, string userName, string password, double longitude, double latitude, string authToken)
        {
            // only validated user can post destinations
            if(_cDSUserRepository.ValidateUsersCarOwnershipInCDS(userName, password, vin, authToken))
            {
                // check vehicles current position ->request to Status -> gps_longitude & gps_latitude
                status.Gps_Latitude = _sqlStatusRepository.getLatitudeFromDB(vin);
                status.Gps_Longitude = _sqlStatusRepository.getLongitudeFromDB(vin);

                if (status.Gps_Latitude != 0.0 && status.Gps_Longitude != 0.0)
                {
                    // send and log destination in DB table Routes
                    if (longitude != null && latitude != null)
                    {
                        _sqlStatusRepository.logDestinationInRouteTable(vin, longitude, latitude);

                        return new OkObjectResult("New destination send to DB");
                    }
                    return new BadRequestObjectResult("Cannot process your request - missing values?");

                }
                return new NotFoundObjectResult("There's no current GPS coordinates for that vehicle");
            }
            return new UnauthorizedObjectResult("Unauthorized user access to CDS");
        }

        [HttpGet]
        [Route("{vin}/destination")]
        public ActionResult<string> GetDestinationsFromDB(string vin)
        {
            // vehicle gets the destinations
            if (vin != null)
            {
                string destination = _sqlVehicleRepository.GetNewRoutesDestination(vin);
            }
            return new NotFoundObjectResult("Nothing to return");
        }

    }
}
