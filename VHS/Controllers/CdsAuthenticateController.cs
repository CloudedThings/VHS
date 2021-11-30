using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHS.Entity.Cds;
using VHSBackend.Core.Integrations;
using VHSBackend.Web.Attributes;
using VHS.Entity.Cds;
using VHSBackend.Core;
using VHS.Entity;
using Newtonsoft.Json;
using VHSBackend.Core.Repository;

namespace VHSBackend.Web.Controllers
{
    [Route("api/cdsauthenticate")]
    [ApiController]
    public class CdsAuthenticateController : ControllerBase
    {
        SqlVehicleRepository sqlVehicleRepository = new SqlVehicleRepository();
        public CdsAuthenticateController()
        {
            _cdsClient = new CdsClient();
        }
        private readonly CdsClient _cdsClient;

        [HttpGet]
        [Route("login")]
        public ActionResult<LoginResponse> LoginCds(string userName, string password)
        {
            var result = _cdsClient.Login(userName, password);
            if (result != null)
            {
                ServiceProvider.Current.InMemoryStorage.AddToken(result.AccessToken, result.Id);
                return new OkObjectResult(result);
            }

            return new UnauthorizedResult();

        }

        [HttpGet]
        [Route("getVin")]
        public ActionResult<IList<Vehicle>> GetVIN(string regNo, string authToken)
        {
            var result = _cdsClient.listVins(regNo, authToken);

            foreach (var vehicle in result)
            {
                Guid guid = sqlVehicleRepository.CreateVehicle(vehicle);
                Console.WriteLine(vehicle.Vin);
                Console.WriteLine(vehicle.owner.FirstName);
            }

            if (result != null)
            {
                return new OkObjectResult(result);
            }
            return new UnauthorizedResult();
        }

    }
}
