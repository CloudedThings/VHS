using HiQ.NetStandard.Util.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VHS.Entity;

namespace VHSBackend.Core.Repository
{
    public class SqlVehicleRepository : ADbRepositoryBase
    {
        public Guid CreateVehicle(Vehicle vehicle)
        {
            var parameters = new SqlParameters();
            parameters.AddNVarChar("@vin", 50, vehicle.Vin);
            parameters.AddNVarChar("@id", vehicle.owner.Id, 50, ParameterDirection.Output);
            //parameters.AddNVarChar("@displayName", vehicle.owner.FirstName, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("dbo.sCreateCars", ref parameters, CommandType.StoredProcedure);


            return new Guid();
        }
    }
}
