using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHS.Entity;
using VHSBackend.Core.Repository;

namespace VHSBackend.Web.Controllers
{
    [Route("api/drivingrecords")]
    [ApiController]
    public class DrivingRecordsController : ControllerBase
    {
        public DrivingRecordsController()
        {
            _sqlDrivingRecordsRepository = new SqlDrivingRecordsRepository();
        }

        private readonly SqlDrivingRecordsRepository _sqlDrivingRecordsRepository;
        
        
        // starta körningen 
        [HttpPost]
        [Route("{vin}/startTrip")]
        public ActionResult<Guid> StartJournal(string vin)
        {
            Guid journal_id = _sqlDrivingRecordsRepository.StartDrivingJournal(vin);
            // return journal_id och posta den första loggen med starttid
            
            
            return new OkObjectResult(journal_id);
        }

        // under resans gång
        [HttpPost]
        [Route("{vin}/{journal_id}/triplogs")]
        public ActionResult<bool> sendRegularLogsUnderTrip(string vin, Guid journal_id, DriveLogData logData)
        {
            _sqlDrivingRecordsRepository.sendDrivingLogs(vin, journal_id, logData);
            //sendDrivingLogs(vin)
            return new OkObjectResult(true);
        }

        // avsluta 
        [HttpPost]
        [Route("{vin}/{journal_id}/savetrip")]
        public ActionResult<IList<DriveLogData>>  SaveTrip(string vin, Guid journal_id)
        {

            IList<DriveLogData> tripLogs = _sqlDrivingRecordsRepository.GetTripLogs(vin, journal_id);

            return new OkObjectResult(tripLogs);
        }
    }
}
