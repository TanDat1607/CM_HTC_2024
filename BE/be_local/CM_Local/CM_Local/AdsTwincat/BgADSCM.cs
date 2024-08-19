using CM_Local.ConnectDB;
using CM_Local.Entity;

namespace CM_Local.AdsTwincat
{
    public class BgADSCM : BackgroundService
    {
        private readonly FB_ADS fb_ads;
        ConnectMongo ConnectDB = new ConnectMongo();
        public readonly string amsNetId = File.ReadAllText("Settings/netid.txt");
        public readonly int port = int.Parse(File.ReadAllText("Settings/portCm.txt"));
        public readonly int ncountpoint = int.Parse(File.ReadAllText("Settings/nCountpoint.txt"));
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string url = File.ReadAllText("Settings/urlapi.txt");

        CancellationTokenSource cts = new CancellationTokenSource();

        public BgADSCM(FB_ADS adsService)
        {
            fb_ads = adsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task t1 = RefreshUrlPeriodically(url, 10000, cts.Token);
            // continuous execution of background task
            while (!cts.Token.IsCancellationRequested)
            {
                try
                {
                    // ADS connection monitoring
                    Task t0 = AdsMonitor(100, cts.Token);
                    
                    await Task.WhenAll(t0);
                    //await Task.WhenAll(new[] { t0 });
                }
                catch (TaskCanceledException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private async Task AdsMonitor(int delay, CancellationToken token)
        {
            while (!cts.IsCancellationRequested)
            {
                var adsConnect = fb_ads.AdsConnect(amsNetId, port);
                try
                {
                    if (adsConnect)
                    {
                        bool bCheckADS = (bool)fb_ads.AdsRead(fb_ads.tcAdsClient, "GVL_ADS.bCheckADS", typeof(bool));
                        if (!bCheckADS)
                        {
                            fb_ads.AdsWrite(fb_ads.tcAdsClient, "GVL_ADS.bCheckADS", true);
                        }
                        bool bAddData = (bool)fb_ads.AdsRead(fb_ads.tcAdsClient, "GVL_ADS.bAddData", typeof(bool));
                        if (bAddData)
                        {
                            Point point = new Point();
                            point.DateTime = DateTime.Now;
                            //CM
                            for (int i = 1; i <= ncountpoint; i++)
                            {
                                point.FFTAcc = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTAcceleration" + i + "Add", typeof(Double[]))));
                                point.FFTVel = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTVelocity" + i + "Add", typeof(Double[]))));
                                point.FFTEnv = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTEnvelope" + i + "Add", typeof(Double[]))));
                                point.RMSAcc = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSAcc" + i + "Add", typeof(Double)));
                                point.RMSVel = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSVel" + i + "Add", typeof(Double)));
                                point.RMSEnv = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSEnv" + i + "Add", typeof(Double)));
                                point.Temperature = (float)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.Temperature" + i + "Add", typeof(float)));
                           //     await fbpoint.InsertDB($"{i}", point);
                                if (i == 1) { ConnectDB.InsertPoint1(point);}
                                else if (i ==2) {ConnectDB.InsertPoint2(point);}
                            }
                            
                            //false bAddData
                            fb_ads.AdsWrite(fb_ads.tcAdsClient, "GVL_ADS.bAddData", false);
                        }
                    }
                }
                catch (Exception err)
                {
                    System.Diagnostics.Debug.WriteLine(err.Message);
                }
                await Task.Delay(delay, token);
            }
        }

        private async Task RefreshUrlPeriodically(string url, int delay, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await RefreshUrl(url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to refresh URL: {ex.Message}");
                }
                await Task.Delay(delay, token);
            }
        }

        private async Task RefreshUrl(string url)
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            System.Diagnostics.Debug.WriteLine("Refreshed URL successfully.");
        }

        public async void ResetTask()
        {
            cts.Cancel();
            cts.Dispose();
            cts = new CancellationTokenSource();
            await ExecuteAsync(cts.Token);
        }
    }
}
