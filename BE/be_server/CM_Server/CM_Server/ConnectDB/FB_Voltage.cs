// using CM_Server.Entity;
// using System.Data;
// using CM_Server.Services;
//
// namespace CM_Server.ConnectDB
// {
//     public class FB_Voltage
//     {
//         Connect Db = new Connect();
//         private readonly string logFile = "Logs/log.txt";
//         public async Task<bool> InsertDB(Voltage_get voltage)
//         {
//             Logger.LogFile(logFile, $@"Insert Data Voltage");
//             bool checkdb = await Db.ExeQuery($@"INSERT INTO [CM_Server].[dbo].[Voltage] (Datetime, FFTU1, FFTU2, FFTU3, RMSU1, RMSU2, RMSU3, Frequency) VALUES ('{voltage.DateTime}','{voltage.FFTU1}','{voltage.FFTU2}','{voltage.FFTU3}','{voltage.RMSU1}','{voltage.RMSU2}','{voltage.RMSU3}','{voltage.Frequency}')");
//             if (checkdb)
//             {
//                 Logger.LogFile(logFile, $@"INSERT Voltage Completed");
//                 return true;
//             }
//             else
//             {
//                 Logger.LogFile(logFile, $@"INSERT Voltage Failed");
//                 return false;
//             }
//         }
//         //public async Task<DataTable> GetAllTableAsync(string Point)
//         //{
//         //    DataTable Data = new DataTable();
//         //    Data = await Db.GetDataTable(@"SELECT * FROM [CM].[dbo].[Point_" + Point + "]");
//         //    Logger.LogFile(logFile, $@"Read All Data Point_{Point}");
//         //    return Data;
//         //}
//         public async Task<DataTable> GetDataAsync(string Value)
//         {
//             DataTable Data = new DataTable();
//             Data = await Db.GetDataTable($@"SELECT * FROM [CM_Server].[dbo].[Voltage] WHERE DateTime = '{Value}'");
//             Logger.LogFile(logFile, $@"ReadData Voltage-DateTime:{Value}");
//             return Data;
//         }
//         public async Task<DataTable> GetDataStore(string Value, string Startdate, string Enddate)
//         {
//             DataTable Data = new DataTable();
//             Data = await Db.GetDataTable(@$"EXEC [dbo].[Get{Value}] '{Startdate}','{Enddate}'");
//             Logger.LogFile(logFile, $@"ReadData-{Value}-from:{Startdate} to :{Enddate}");
//             return Data;
//         }
//     }
// }
