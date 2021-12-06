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
        private readonly SqlVehicleRepository _vehicleRepository;

        public StatusController()
        {
            _vehicleRepository = new SqlVehicleRepository();
            _cdsClient = new CdsClient();
        }
        private readonly CdsClient _cdsClient;

        [HttpPost]
        [Route("{vin}/status/summary")]
        public ActionResult<bool> UpdateStatus(string vin, bool lockStatus, 
            int battery, double longitude, double latitude, bool alarm, string tirePressure,
            double milage, string authToken)
        {
            // No status change can be done on cars without owner
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateSummaryStatusInDB(vin, lockStatus, battery, longitude, latitude, alarm, tirePressure, milage);
                return new OkObjectResult($"Status summary changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - summary status cannot be changed!");
        }

        [HttpPost]
        [Route("{vin}/status/lock")]
        public ActionResult<bool> UpdateLockStatus(string vin, bool lockStatus, string authToken)
        {
            // No status change can be done on cars without owner
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateLockStatusInDB(vin, lockStatus);
                return new OkObjectResult($"Lock status changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - lock status cannot be changed!");
        }


        [HttpPost]
        [Route("{vin}/status/battery")]
        public ActionResult<bool> UpdateBatteryStatus(string vin, int batteryLevel, string authToken)
        {
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateBatteryStatusInDB(vin, batteryLevel);
                return new OkObjectResult($"Battery status changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - Battery status cannot be changed!");
        }

        [HttpPost]
        [Route("{vin}/status/alarm")]
        public ActionResult<bool> UpdateAlarmStatus(string vin, bool alarm, string authToken)
        {
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateAlarmStatusInDB(vin, alarm);
                return new OkObjectResult($"Battery status changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - Battery status cannot be changed!");
        }

        [HttpPost]
        [Route("{vin}/status/tirepressure")]
        public ActionResult<bool> UpdateTirePressureStatus(string vin, string pressure, string authToken)
        {
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateTirePressureStatusInDB(vin, pressure);
                return new OkObjectResult($"Battery status changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - Battery status cannot be changed!");
        }

        [HttpPost]
        [Route("{vin}/status/milage")]
        public ActionResult<bool> UpdateMilageStatus(string vin, float milage, string authToken)
        {
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateMilageStatusInDB(vin, milage);
                return new OkObjectResult($"Battery status changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - Battery status cannot be changed!");
        }

        [HttpPost]
        [Route("{vin}/status/gps")]
        public ActionResult<bool> UpdateGpsStatus(string vin, double longitude, double latitude, string authToken)
        {
            // No status change can be done on cars without owner
            if (_vehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            {
                _vehicleRepository.UpdateGpsStatusInDB(vin, longitude, latitude);
                return new OkObjectResult($"GPS position changed");
            }

            return new NotFoundObjectResult("Vehicle has no owner - GPS status cannot be changed!");
        }

        [HttpGet]
        [Route("{vin}/status")]
        public ActionResult<string> GetStatus(string vin)
        {
            var result = _vehicleRepository.GetStatus(vin);
            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new NotFoundObjectResult("Not found!");
        }

        [HttpPost]
        [Route("{vin}/commands")]
        public ActionResult<string> SendCommand(string vin, string userName, string password, string action, bool value)
        {


            return new OkObjectResult("OK");
        }

    }

}
