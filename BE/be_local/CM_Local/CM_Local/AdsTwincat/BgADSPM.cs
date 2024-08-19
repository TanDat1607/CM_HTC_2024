using CM_Local.ConnectDB;
using CM_Local.Entity;

namespace CM_Local.AdsTwincat
{
    public class BgADSPM : BackgroundService
    {
        private readonly FB_ADS1 fb_ads;
        ConnectMongo ConnectDB = new ConnectMongo();
        public readonly string amsNetId = File.ReadAllText("Settings/netid.txt");
        public readonly int port = int.Parse(File.ReadAllText("Settings/portPm.txt"));

        CancellationTokenSource cts = new CancellationTokenSource();

        public BgADSPM(FB_ADS1 adsService)
        {
            fb_ads = adsService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
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
                            Current current = new Current();
                            Voltage voltage = new Voltage();
                            current.DateTime = DateTime.Now;
                            voltage.DateTime = DateTime.Now;
                            //PM-Current
                            current.FFTI1 = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTI1", typeof(Double[]))));
                            current.FFTI2 = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTI2", typeof(Double[]))));
                            current.FFTI3 = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTI3", typeof(Double[]))));
                            current.RMSI1 = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSI1", typeof(Double)));
                            current.RMSI2 = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSI2", typeof(Double)));
                            current.RMSI3 = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSI3", typeof(Double)));
                            current.Frequency = (float)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.Frequency", typeof(float)));
                            //PM-Voltage
                            voltage.FFTU1 = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTU1", typeof(Double[]))));
                            voltage.FFTU2 = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTU2", typeof(Double[]))));
                            voltage.FFTU3 = string.Join(", ", (Double[])(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.aFFTU3", typeof(Double[]))));
                            voltage.RMSU1 = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSU1", typeof(Double)));
                            voltage.RMSU2 = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSU2", typeof(Double)));
                            voltage.RMSU3 = (Double)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.RMSU3", typeof(Double)));
                            voltage.Frequency = (float)(fb_ads.AdsRead(fb_ads.tcAdsClient, @"GVL_ADS.Frequency", typeof(float)));
                            //add PM
                        //    await fbcurrent.InsertDB(current);
                        //    await fbvoltage.InsertDB(voltage);
                            ConnectDB.InsertCurrent(current);
                            ConnectDB.InsertVoltage(voltage);
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

        public async void ResetTask()
        {
            cts.Cancel();
            cts.Dispose();
            cts = new CancellationTokenSource();
            await ExecuteAsync(cts.Token);
        }
    }
}
