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
        public ActionResult<bool> StartJournal(string vin, DrivingJournal journal)
        {
            // var journal_id = startDrivingJournal(vin) -> returnerar journal_id
            Guid journal_id = _sqlDrivingRecordsRepository.StartDrivingJournal(vin);
            // return journal_id och posta den först loggen med starttid
            // sendDrivingLogs(vin, journal_id) -> den första loggen i DrivingLogs

            return new OkObjectResult(journal_id);
        }

        // under resans gång
        [HttpPost]
        [Route("{vin}/triplogs")]
        public ActionResult<bool> sendRegularLogsUnderTrip(string vin)
        {
            //sendDrivingLogs(vin)
            return new BadRequestResult();
        }

        // avsluta 
        [HttpPost]
        [Route("{vin}/savetrip")]
        public ActionResult<bool>  SaveTrip(string vin)
        {
            return new BadRequestResult();
        }
    }
}
