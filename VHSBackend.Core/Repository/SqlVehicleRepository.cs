using HiQ.NetStandard.Util.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VHS.Entity;
using VHSBackend.Core.Integrations;

namespace VHSBackend.Core.Repository
{
    public class SqlVehicleRepository : ADbRepositoryBase, IVehicleRepository
    {
        private readonly SqlVehicleRepository _vehicleRepository = new SqlVehicleRepository();

        public SqlVehicleRepository()
        {
            _cdsClient = new CdsClient();
        }
        private readonly CdsClient _cdsClient;

        public bool CheckIfCarHasAnOwnerInCDS(string vin, string authToken)
        {
            var ownerStatus = 0;
            var regNo = _vehicleRepository.SearchVehicle(vin);
            if (regNo.ToLower().Equals("invalid vin input"))
            {
                return false;
            }

            var responseFromCDS = _cdsClient.ownerData(regNo, vin, authToken);
            if (responseFromCDS.owner == null)
            {
                ownerStatus = 0;
            }
            ownerStatus = responseFromCDS.owner.OwnerStatus;

            if (ownerStatus == 0)
            {
                return false;
            } else
            {
                return true;
            }
        }

        public void InsertLockStatusInDB(string vin, bool lockStatus)
        {
            var parameters = new SqlParameters();
            parameters.AddNVarChar("@vin", 50, vin);
            parameters.AddBoolean("@lock", lockStatus);
            DbAccess.ExecuteNonQuery("dbo.sInsertLockStatus", ref parameters, CommandType.StoredProcedure);
        }

        public string SearchVehicle(string vin)
        {
            if(!string.IsNullOrEmpty(vin))
            {
                var parameters = new SqlParameters();
                parameters.AddNVarChar("@vin", 50, vin);
                parameters.AddNVarChar("@regNo", 50, "", ParameterDirection.Output);
                parameters.AddBoolean("@Result", false, ParameterDirection.Output);
                DbAccess.ExecuteNonQuery("dbo.sGetRegNo", ref parameters, CommandType.StoredProcedure);
                if(parameters.GetBool("@Result") )
                {
                    var regNo = parameters.GetString("@regNo");
                    return regNo;
                }
            }
            var response = "Invalid vin input";
            return response;
        }


        public Guid CreateVehicle(Vehicle vehicle)
        {
            var parameters = new SqlParameters();
            
            parameters.AddNVarChar("@vin", 50, vehicle.Vin);
           
            
            if (vehicle.owner != null && !string.IsNullOrEmpty(vehicle.owner.Id))
            {
                parameters.AddNVarChar("@id", vehicle.owner.Id, 50, ParameterDirection.Input);
                parameters.AddNVarChar("@displayName", vehicle.owner.FirstName, 50, ParameterDirection.Input);
            }
            else
            {
                parameters.AddNVarChar("@id", null, 50, ParameterDirection.Input);
                parameters.AddNVarChar("@displayName", null, 50, ParameterDirection.Input);
            }
            parameters.AddNVarChar("@regNo", 50, vehicle.Regno);




            DbAccess.ExecuteNonQuery("dbo.sAddVehicle", ref parameters, CommandType.StoredProcedure);


            return new Guid();
        }

    }
}
