using CM_Local.Entity;
using System.Data;
using CM_Local.Services;

namespace CM_Local.ConnectDB
{
    public class FB_Current
    {
        Connect1 Db = new Connect1();
        private readonly string logFile = "Logs/log.txt";
        public async Task<DataTable> GetSyncCurrent()
        {
            DataTable DataAll = new DataTable();
            DataAll = await Db.GetDataTable("DELETE FROM [CM].[dbo].[Current] OUTPUT deleted.*");
            return DataAll;
        }
        public async Task<bool> InsertDB(Current current)
        {
            Logger.LogFile(logFile, $@"Insert Data Current");
            bool checkdb = await Db.ExeQuery($@"INSERT INTO [CM].[dbo].[Current] (Datetime, FFTI1, FFTI2, FFTI3, RMSI1, RMSI2, RMSI3, Frequency) VALUES ('{current.DateTime}','{current.FFTI1}','{current.FFTI2}','{current.FFTI3}','{current.RMSI1}','{current.RMSI2}','{current.RMSI3}','{current.Frequency}')");
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
        //public async Task<DataTable> GetDataAsync(string Point,string Value)
        //{
        //    DataTable Data = new DataTable();
        //    Data = await Db.GetDataTable(@"SELECT * FROM [CM].[dbo].[Point_" + Point + "] WHERE DateTime = '" + Value + "'");
        //    Logger.LogFile(logFile, $@"ReadData Point_{Point}-DateTime:{Value}");
        //    return Data;
        //}
        //public async Task<DataTable> GetDataStore(string Point, string Value, string Startdate, string Enddate)
        //{
        //    DataTable Data = new DataTable();
        //    Data = await Db.GetDataTable(@$"EXEC [dbo].[Get{Value}DataPoint{Point}] '{Startdate}','{Enddate}'");
        //    Logger.LogFile(logFile, $@"ReadData-{Value}-Point_{Point}--from:{Startdate} to :{Enddate}");
        //    return Data;
        //}
    }
}
