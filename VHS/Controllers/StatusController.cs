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
    [Route("api/vehicle")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly SqlVehicleRepository _vehicleRepository = new SqlVehicleRepository();

        public StatusController()
        {
            _cdsClient = new CdsClient();
        }
        private readonly CdsClient _cdsClient;

        [HttpPost]
        [Route("{vin}/status/lock")]
        public ActionResult<bool> PostLockStatus(string vin, bool lockStatus, string authToken)
        {
            if(_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken)) {
                _vehicleRepository.InsertLockStatusInDB(vin, lockStatus);
                return new OkObjectResult($"Added!");
            }

            return new NotFoundObjectResult("No vehicle found!");



            // var regNo = _vehicleRepository.SearchVehicle(vin);
            //if (regNo != null)
            //{

            //} else {
            //    return new NotFoundObjectResult("Not found!");
            //}
            // if (!CheckIfCarExistsInCDS(regNo)) {
            //    return new NotFoundObjectResult("There's no matching vehicle in CDS!");
            // } else {
            //  postNewLockStatusIntoDB(lockStatus);
            // }
            //  return OkObjectResult("Lock status updated!");
        }

        //[HttpPost]
        //[Route("status")]
        //public ActionResult<bool> PostStatus(VIN vin)
        //{
        //    return new OkObjectResult(vin);
        //}


        //[HttpPost]
        //[Route("greet")]
        //public ActionResult<string> Greet(string text)
        //{
        //    return new OkObjectResult($"Hello {text}");
        //}
    }


}
