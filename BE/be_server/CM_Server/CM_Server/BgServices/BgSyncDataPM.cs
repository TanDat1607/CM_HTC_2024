using CM_Server.ConnectDB;
using CM_Server.Entity;
using Newtonsoft.Json;

namespace CM_Server.BgServices
{
    public class BgSyncDataPM: BackgroundService
    {
        public readonly string[] apiUrl = File.ReadAllLines("Settings/urlapi.txt");
        HttpClient client = new HttpClient();
        ConnectMongo ConnectDB = new ConnectMongo();
        CancellationTokenSource cts = new CancellationTokenSource();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // continuous execution of background task
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    // ADS connection monitoring
                    Task t1 = SyncMonitor(5000, cts.Token);
                    await Task.WhenAll(t1);
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        private async Task SyncMonitor(int delay, CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now);
            List<Task> tasks = new List<Task>();
            foreach (string lineurl in apiUrl)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        List<Current> currents = new List<Current>();
                        List<Voltage> voltages = new List<Voltage>();
                        //====================
                        //sync current
                        HttpResponseMessage syncCurrent = await client.GetAsync($"{lineurl}/api/pm/SyncCurrent", token);
                        if (syncCurrent.IsSuccessStatusCode)
                        {
                            // Đọc nội dung JSON từ phản hồi
                            string jsoncurrent = await syncCurrent.Content.ReadAsStringAsync();

                            // Phân tích nội dung JSON thành mảng đối tượng sử dụng Json.NET
                            currents = JsonConvert.DeserializeObject<List<Current>>(jsoncurrent);
                            foreach (var currentget in currents)
                            { ConnectDB.InsertCurrent(currentget); }
                        }
                        //sync current
                        HttpResponseMessage syncVoltage = await client.GetAsync($"{lineurl}/api/pm/SyncVoltage", token);
                        if (syncVoltage.IsSuccessStatusCode)
                        {
                            // Đọc nội dung JSON từ phản hồi
                            string jsonvoltage = await syncVoltage.Content.ReadAsStringAsync();

                            // Phân tích nội dung JSON thành mảng đối tượng sử dụng Json.NET
                            voltages = JsonConvert.DeserializeObject<List<Voltage>>(jsonvoltage);
                            foreach (var voltageget in voltages)
                            {
                                ConnectDB.InsertVoltage(voltageget);
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        System.Diagnostics.Debug.WriteLine(DateTime.Now);
                        System.Diagnostics.Debug.WriteLine(err.Message);
                    }
                }));
            }
            await Task.Delay(delay);
        }
    }
}
