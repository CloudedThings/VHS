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

namespace VHSBackend.Web.Controllers
{
    [Route("api/cdsauthenticate")]
    [ApiController]
    public class CdsAuthenticateController : ControllerBase
    {
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
            if (result != null)
            {
            return new OkObjectResult(result);
            }
            return new UnauthorizedResult();
        }


    }
}
