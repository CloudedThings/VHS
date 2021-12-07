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
    [Route("api/commands")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        public CommandsController()
        {
            _cdsClient = new CdsClient();
            _sqlVehicleRepository = new SqlVehicleRepository();
            _cDSUserRepository = new CDSUserRepository();
            _sqlCommandRepository = new SqlCommandRepository();
        }
        private readonly CdsClient _cdsClient;
        private readonly SqlVehicleRepository _sqlVehicleRepository;
        private readonly CDSUserRepository _cDSUserRepository;
        private readonly SqlCommandRepository _sqlCommandRepository;

        // Post endpoint for user/app where commands will be send
        [HttpPost]
        [Route("{vin}")]
        public ActionResult<string> SendCommand(string vin, string userName, string password, Command command, string action, string authToken)
        {
            if (_cDSUserRepository.ValidateUsersCarOwnershipInCDS(userName, password, vin, authToken))
            {
                // Här ska vi skicka tutta och blinka till DB
                // Vi behöver en metod, stored procedure för det och en tabell
                // vi kör antingen if satser eller switch baserad på "action" string

                _sqlCommandRepository.UpdateSpecificActionCommandInDB(vin, command, action);

                return new OkObjectResult("Yes you own a car");
            }
            return new BadRequestObjectResult("This user does not own a car");

            //var result = _cdsClient.Login(userName, password);

            //if (result != null)
            //{
            //    Guid id = result.Id;
            //    var response = _cdsClient.ValidateToken(id, result.AccessToken);
            //    if (_sqlVehicleRepository.CheckIfCarHasAnOwnerInCDS(vin, authToken))
            //    {
            //        // Här ska vi skicka tutta och blinka till DB
            //        // Vi behöver en metod, stored procedure för det och en tabell
            //        // vi kör antingen if satser eller switch baserad på "action" string
            //        return new OkObjectResult("Yes you own a car");
            //    }
            //    return new BadRequestObjectResult("This user does not own a car");

            //}
            //return new UnauthorizedObjectResult("Unauthorized validation in CDS");
        }

        // Get endpoint for car
        [HttpGet]
        [Route("{vin}")]
        public ActionResult<string> GetCommand(string vin)
        {

            // här behöver vi en metod där bilen hämtar commando


            return new OkObjectResult("OK");
        }
        // Post endpoint for car to confirm actions execution
        [HttpPost]
        [Route("{vin}/reset")]
        public ActionResult<string> ResetCommand(string vin, string action, bool value)
        {

            // metod som resetar alla kommando till 0


            return new OkObjectResult("OK");
        }
    }
}
