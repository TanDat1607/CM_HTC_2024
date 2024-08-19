using CM_Local.Entity;
using System.Data;
using CM_Local.Services;

namespace CM_Local.ConnectDB
{
    public class FB_Voltage
    {
        Connect1 Db = new Connect1();
        private readonly string logFile = "Logs/log.txt";
        public async Task<DataTable> GetSyncVoltage()
        {
            DataTable DataAll = new DataTable();
            DataAll = await Db.GetDataTable("DELETE FROM [CM].[dbo].[Voltage] OUTPUT deleted.*");
            return DataAll;
        }
        public async Task<bool> InsertDB(Voltage voltage)
        {
            Logger.LogFile(logFile, $@"Insert Data Voltage");
            bool checkdb = await Db.ExeQuery($@"INSERT INTO [CM].[dbo].[Voltage] (Datetime, FFTU1, FFTU2, FFTU3, RMSU1, RMSU2, RMSU3, Frequency) VALUES ('{voltage.DateTime}','{voltage.FFTU1}','{voltage.FFTU2}','{voltage.FFTU3}','{voltage.RMSU1}','{voltage.RMSU2}','{voltage.RMSU3}','{voltage.Frequency}')");
            if (checkdb)
            {
                Logger.LogFile(logFile, $@"INSERT Voltage Completed");
                return true;
            }
            else
            {
                Logger.LogFile(logFile, $@"INSERT Voltage Failed");
                return false;
            }
        }
        //public async Task<DataTable> GetAllTableAsync(string Point)
        //{
        //    DataTable Data = new DataTable();
        //    Data = await Db.GetDataTable(@"SELECT * FROM [CM].[dbo].[Point_" + Point + "]");
        //    Logger.LogFile(logFile, $@"Read All Data Point_{Point}");
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
