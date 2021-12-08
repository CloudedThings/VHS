﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VHS.Entity.Cds;
using VHSBackend.Core.Integrations;
using VHSBackend.Web.Attributes;
using VHSBackend.Core;
using VHS.Entity;
using Newtonsoft.Json;
using VHSBackend.Core.Repository;

namespace VHSBackend.Web.Controllers
{
    [Route("api/cds")]
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

        //[HttpGet]
        //[Route("getVin")]
        //public ActionResult<IList<Vehicle>> GetVIN(string regNo, string authToken)
        //{
        //    if (authToken != null)
        //    {
        //        var result = _cdsClient.listVins(regNo, authToken);
        //        if (result != null)
        //        {
        //            foreach (var vehicle in result)
        //            {
        //                // OBS! Special case för Kim och Mattias som är delägarna till en bil...
        //                // TODO
        //                if (vehicle.Vin == "23826029")
        //                {
        //                    var rand = new Random();
        //                    vehicle.Vin = rand.Next(1000000, 10000000).ToString();
        //                }
        //                Guid guid = sqlVehicleRepository.CreateVehicle(vehicle);
        //            }
        //            return new OkObjectResult(result);
        //        }
        //    }
        //    return new UnauthorizedResult();
        //}

        [HttpGet]
        [Route("updateVinTable")]
        public ActionResult<IList<Vehicle>> UpdateVinTable(string regNo, string authToken)
        {
            if (authToken != null)
            {
                var vehicleList = _cdsClient.listVins(regNo, authToken);

                if (vehicleList != null)
                {
                    foreach (var vehicle in vehicleList)
                    {
                        Guid guid = sqlVehicleRepository.UpdateVinList(vehicle);
                    }
                }

                return new OkObjectResult(vehicleList);
            }
            return new UnauthorizedResult();
        }
    }
}
