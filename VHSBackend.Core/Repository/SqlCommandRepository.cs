using HiQ.NetStandard.Util.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VHS.Entity;

namespace VHSBackend.Core.Repository
{
    public class SqlCommandRepository: ADbRepositoryBase
    {
        public Command GetCommand(string vin)
        {
            var parameters = new SqlParameters();
            parameters.AddNVarChar("@vin", 50, vin);
            parameters.AddBoolean("@result", false, ParameterDirection.Output);
            parameters.AddBoolean("@lights", false, ParameterDirection.Output);
            parameters.AddBoolean("@honk", false, ParameterDirection.Output);
            parameters.AddBoolean("@door", false, ParameterDirection.Output);
            parameters.AddBoolean("@heat", false, ParameterDirection.Output);
            parameters.AddBoolean("@ac", false, ParameterDirection.Output);
            parameters.AddBoolean("@trunk", false, ParameterDirection.Output);
            parameters.AddBoolean("@getDest", false, ParameterDirection.Output);
            parameters.AddDateTime("@DateLastModified", DateTime.Now, ParameterDirection.Output);

            DbAccess.ExecuteNonQuery("dbo.sGetCommands", ref parameters, CommandType.StoredProcedure);

            if (parameters.GetBool("@result"))
            {
                var command = new Command();
                command.Vin = parameters.GetString("@vin");
                command.Lights = parameters.GetBool("@lights");
                command.Honk = parameters.GetBool("@honk");
                command.Door = parameters.GetBool("@door");
                command.Heat = parameters.GetBool("@heat");
                command.AC = parameters.GetBool("@ac");
                command.Trunk = parameters.GetBool("@trunk");
                command.GetDest = parameters.GetBool("@getDest");
                command.DateLastModified = parameters.GetDateTime("@DateLastModified");

                return command;
            }
            return null;
        }

        public bool UpdateCommands(Command command, string vin)
        {
            var parameters = new SqlParameters();
            parameters.AddNVarChar("@vin", 50, vin);
            parameters.AddBoolean("@lights", command.Lights, ParameterDirection.Input);
            parameters.AddBoolean("@honk", command.Honk, ParameterDirection.Input);
            parameters.AddBoolean("@door", command.Door, ParameterDirection.Input);
            parameters.AddBoolean("@heat", command.Heat, ParameterDirection.Input);
            parameters.AddBoolean("@ac", command.AC, ParameterDirection.Input);
            parameters.AddBoolean("@trunk", command.Trunk, ParameterDirection.Input);
            parameters.AddBoolean("@getDest", command.GetDest, ParameterDirection.Input);
            
            DbAccess.ExecuteNonQuery("dbo.sUpdateCommands", ref parameters, CommandType.StoredProcedure);

            return true;
            
        }

        public bool UpdateSpecificActionCommandInDB(string vin, Command command, string action)
        {
            // here Send specific action and change value for that action?
            var currentCommand = GetCommand(vin);
            // TODO
            //command.${action} = 1 || 0;

            if(UpdateCommands(command, vin))
            {
                return true;
            }
            return false;

        }
    }
}
