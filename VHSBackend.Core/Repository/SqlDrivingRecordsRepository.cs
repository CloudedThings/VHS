using HiQ.NetStandard.Util.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace VHSBackend.Core.Repository
{
    public class SqlDrivingRecordsRepository : ADbRepositoryBase
    {
        public Guid StartDrivingJournal(string vin)
        {
            Guid journal_id = Guid.NewGuid();
            var parameters = new SqlParameters();
            parameters.AddNVarChar("@vin", vin, 50, ParameterDirection.Input);
            parameters.AddUniqueIdentifier("@journal_id", journal_id, ParameterDirection.Input);
            parameters.AddDateTime("@startTime", DateTime.Now, ParameterDirection.Input);
            parameters.AddDateTime("@endTime", null, ParameterDirection.Input);
            parameters.AddInt("@tripStatus", 0, ParameterDirection.Input);
            parameters.AddInt("@tripDistance", 0, ParameterDirection.Input);
            parameters.AddInt("@tripEnergyConsumption", 0, ParameterDirection.Input);
            parameters.AddInt("@tripAverageEnergyCons", 0, ParameterDirection.Input);
            parameters.AddInt("@tripAverageSpeed", 0, ParameterDirection.Input);
            parameters.AddDateTime("@tripDate", null, ParameterDirection.Input);

            DbAccess.ExecuteNonQuery("dbo.sStartDrivingJournal", ref parameters, CommandType.StoredProcedure);
            return journal_id;
        }
    }
}
