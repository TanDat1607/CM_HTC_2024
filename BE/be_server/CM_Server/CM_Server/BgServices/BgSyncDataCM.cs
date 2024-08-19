using CM_Server.ConnectDB;
using CM_Server.Entity;
using CM_Server.Services;
using Newtonsoft.Json;

namespace CM_Server.BgServices
{
    public class BgSyncDataCm: BackgroundService
    {
        public readonly string[] ApiUrl = File.ReadAllLines("Settings/urlapi.txt");
        public readonly int Ncountpoint = int.Parse(File.ReadAllText("Settings/nCountpoint.txt"));
        private const string LogPath = ConnectMongo.LogFile;
        HttpClient _client = new HttpClient();
        ConnectMongo _connectDb = new ConnectMongo();

        readonly CancellationTokenSource _cts = new CancellationTokenSource();
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // continuous execution of background task
            while (!_cts.Token.IsCancellationRequested)
            {
                try
                {
                    // ADS connection monitoring
                    Task t1 = SyncMonitor(5000, _cts.Token);
                    await Task.WhenAll(t1);
                }
                catch (TaskCanceledException ex)
                { Console.WriteLine(ex.ToString()); }
            }
        }
        private async Task SyncMonitor(int delay, CancellationToken token)
        {
            System.Diagnostics.Debug.WriteLine(DateTime.Now);
            List<Task> tasks = new List<Task>();
            foreach (string lineurl in ApiUrl)
            {
                tasks.Add(Task.Run(async () =>
                {
                    try
                    {
                        List<Point> points = new List<Point>();
                        //sync 
                        for (int i = 1; i <= Ncountpoint; i++)
                        {
                            HttpResponseMessage syncpoint = await _client.GetAsync($"{lineurl}/api/cm/Sync/{i}", token);
                            if (syncpoint.IsSuccessStatusCode)
                            {
                                // Đọc nội dung JSON từ phản hồi
                                string jsonpoint = await syncpoint.Content.ReadAsStringAsync();
                                // Phân tích nội dung JSON thành mảng đối tượng sử dụng Json.NET
                                points = JsonConvert.DeserializeObject<List<Point>>(jsonpoint);
                                foreach (var pointget in points)
                                {
                                    switch (i)
                                    {
                                        case 1: _connectDb.InsertPoint1(pointget); break;
                                        case 2: _connectDb.InsertPoint2(pointget); break;
                                    }
                                }
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
