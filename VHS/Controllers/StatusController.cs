using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHS.Entity;


namespace VHSBackend.Web.Controllers
{
    [Route("api/vin")]
    [ApiController]
    public class StatusController : ControllerBase
    {
      //  public statuscontroller()
      //  {
      //      _statusrepository = new sqlstatusrepository();
      // }

        [HttpPost]
        [Route("status")]
        public ActionResult<bool> PostStatus(VIN vin)
        {
            return new OkObjectResult(vin);
        }

        [HttpPost]
        [Route("greet")]
        public ActionResult<string> Greet(string text)
        {
            return new OkObjectResult($"Hello {text}");
        }


    }


}
