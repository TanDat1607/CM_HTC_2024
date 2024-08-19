using CM_Local.Entity;
using System.Data;
using CM_Local.Services;

namespace CM_Local.ConnectDB
{
    public class FB_Point
    {
        Connect Db = new Connect();
        private readonly string logFile = "Logs/log.txt";
        public async Task<DataTable> GetSync(string Point)
        {
            DataTable DataAll = new DataTable();
            DataAll = await Db.GetDataTable("DELETE FROM [CM].[dbo].[Point_" + Point + "] OUTPUT deleted.*");
            return DataAll;
        }
        public async Task<bool> InsertDB(string Point,Point po)
        {
            Logger.LogFile(logFile, $@"Insert Data Point_{Point}");
            bool checkdb = await Db.ExeQuery(@"INSERT INTO [CM].[dbo].[Point_" + Point + "] (Datetime, FFTAcc, FFTVel, FFTEnv, RMSAcc, RMSVel, RMSEnv, Temperature) VALUES ('" + po.DateTime+"','" + po.FFTAcc + "','" + po.FFTVel + "','" + po.FFTEnv + "','" + po.RMSAcc + "','" + po.RMSVel + "','" + po.RMSEnv + "','" + po.Temperature + "')");
            if (checkdb)
            {
                Logger.LogFile(logFile, $@"INSERT Point_{Point} Completed");
                return true;
            }
            else
            {
                Logger.LogFile(logFile, $@"INSERT Point_{Point} Failed");
                return false;
            }
        }
        public async Task<DataTable> GetAllTableAsync(string Point)
        {
            DataTable Data = new DataTable();
            Data = await Db.GetDataTable(@"SELECT * FROM [CM].[dbo].[Point_" + Point + "]");
            Logger.LogFile(logFile, $@"Read All Data Point_{Point}");
            return Data;
        }
        public async Task<DataTable> GetDataAsync(string Point,string Value)
        {
            DataTable Data = new DataTable();
            Data = await Db.GetDataTable(@"SELECT * FROM [CM].[dbo].[Point_" + Point + "] WHERE DateTime = '" + Value + "'");
            Logger.LogFile(logFile, $@"ReadData Point_{Point}-DateTime:{Value}");
            return Data;
        }
        public async Task<DataTable> GetDataStore(string Point, string Value, string Startdate, string Enddate)
        {
            DataTable Data = new DataTable();
            Data = await Db.GetDataTable(@$"EXEC [dbo].[Get{Value}DataPoint{Point}] '{Startdate}','{Enddate}'");
            Logger.LogFile(logFile, $@"ReadData-{Value}-Point_{Point}--from:{Startdate} to :{Enddate}");
            return Data;
        }
    }
}
