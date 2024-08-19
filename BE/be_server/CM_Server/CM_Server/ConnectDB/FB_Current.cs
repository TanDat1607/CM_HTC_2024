using CM_Server.Entity;
using System.Data;
using CM_Server.Services;

namespace CM_Server.ConnectDB
{
    public class FB_Current
    {
        Connect Db = new Connect();
        private readonly string logFile = "Logs/log.txt";
        public async Task<bool> InsertDB(Current_get current)
        {
            Logger.LogFile(logFile, $@"Insert Data Current");
            bool checkdb = await Db.ExeQuery($@"INSERT INTO [CM_Server].[dbo].[Current] (Datetime, FFTI1, FFTI2, FFTI3, RMSI1, RMSI2, RMSI3, Frequency) VALUES ('{current.DateTime}','{current.FFTI1}','{current.FFTI2}','{current.FFTI3}','{current.RMSI1}','{current.RMSI2}','{current.RMSI3}','{current.Frequency}')");
            if (checkdb)
            {
                Logger.LogFile(logFile, $@"INSERT Current Completed");
                return true;
            }
            else
            {
                Logger.LogFile(logFile, $@"INSERT Current Failed");
                return false;
            }
        }
        //public async Task<DataTable> GetAllTableAsync()
        //{
        //    DataTable Data = new DataTable();
        //    Data = await Db.GetDataTable(@"SELECT * FROM [CM].[dbo].[Current]");
        //    Logger.LogFile(logFile, $@"Read All Data Current");
        //    return Data;
        //}
        public async Task<DataTable> GetDataAsync(string Value)
        {
            DataTable Data = new DataTable();
            Data = await Db.GetDataTable($@"SELECT * FROM [CM_Server].[dbo].[Current] WHERE DateTime = '{Value}'");
            Logger.LogFile(logFile, $@"ReadData Current-DateTime:{Value}");
            return Data;
        }
        public async Task<DataTable> GetDataStore(string Value, string Startdate, string Enddate)
        {
            DataTable Data = new DataTable();
            Data = await Db.GetDataTable(@$"EXEC [dbo].[Get{Value}] '{Startdate}','{Enddate}'");
            Logger.LogFile(logFile, $@"ReadData-{Value}-from:{Startdate} to :{Enddate}");
            return Data;
        }
    }
}
