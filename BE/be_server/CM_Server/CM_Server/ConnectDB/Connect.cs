using System.Data;
using System.Data.SqlClient;
using CM_Server.Services;

namespace CM_Server.ConnectDB
{
    public class Connect
    {
        public static SqlConnection ConnectDB;
        private readonly string logFile = "Logs/log.txt";
        //========================Connect DB==============================
        public async Task MoKetNoi()
        {
            {
                string serverdb = File.ReadAllText("Settings/serverdb.txt");
                string userid = File.ReadAllText("Settings/useriddb.txt");
                string password = File.ReadAllText("Settings/passworddb.txt");
                Connect.ConnectDB = new SqlConnection(@"Server=" + serverdb + ";Database=CM_Server;User Id=" + userid + ";Password=" + password + ";");

            }
            if (Connect.ConnectDB.State != ConnectionState.Open)
            {
               await Connect.ConnectDB.OpenAsync();
            }
        }
        public bool Status()
        {
            if (Connect.ConnectDB.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //========================Disconnect DB==============================
        public async Task DongKetNoi()
        {
            if (Connect.ConnectDB != null)
            {
                if (Connect.ConnectDB.State == ConnectionState.Open)
                {
                   await Connect.ConnectDB.CloseAsync();
                }
            }

        }
        //========================Execute Query DB==============================
        public async Task<bool> ExeQuery(string sQuery)
        {
            try
            {
                await MoKetNoi();
                SqlCommand SqlCMD = new SqlCommand(sQuery, ConnectDB);
                await SqlCMD.ExecuteNonQueryAsync();
                await DongKetNoi();
                return true;
            }
            catch (Exception err)
            {
                System.Diagnostics.Debug.WriteLine(err.Message);
                System.Diagnostics.Debug.WriteLine("SQL sQuery Fail");
                Logger.LogFile(logFile, err.Message);
                return false;
            }
        }
        public async Task<DataTable> GetDataTable(string sQueryGetTable)
        {
            try
            {
                await MoKetNoi();
                using (SqlCommand command = new SqlCommand(sQueryGetTable, ConnectDB))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        DataTable dataTable = new DataTable();
                        dataTable.Load(reader);
                        await DongKetNoi();
                        return dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ ở đây, ví dụ: ghi log
                Console.WriteLine($"Lỗi trong GetDataTable: {ex.Message}");
                Logger.LogFile(logFile, ex.Message);
                return null;
            }
        }
        //========================Get Value DB==============================
        public async Task<string> GetValueAsync(string sValue)
        {
            string temp = null;
            await MoKetNoi();
            SqlCommand SqlCMD = new SqlCommand(sValue, ConnectDB);
            SqlDataReader SqlRead = SqlCMD.ExecuteReader();
            while (SqlRead.Read())
            {
                temp = SqlRead[0].ToString();
            }
            await DongKetNoi();
            return temp;
        }
    }
}
