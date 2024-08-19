using CM_Local.ConnectDB;
using CM_Local.Services;
using CM_Local.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace CM_Local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class cmController : ControllerBase
    {
        [HttpGet("Sync/1")]
        public async Task<IActionResult> Sync1()
        {
            try
            {
               // FB_Point Db = new FB_Point();
                ConnectMongo ConnectDB = new ConnectMongo();
                List<Point_get> pointlistall = new List<Point_get>();
                List<Point> dataAll =  ConnectDB.GetSyncPoint1();
                Logger.LogFile("Logs/log.txt", "Call sync Point 1");
                foreach (var eachpoint in dataAll)
                {
                    Point_get points = new Point_get();
                    {
                        points.DateTime = ((DateTime)eachpoint.DateTime).ToString("yyyy/MM/dd HH:mm");
                        points.FFTAcc = eachpoint.FFTAcc;
                        points.FFTVel = eachpoint.FFTVel;
                        points.FFTEnv = eachpoint.FFTEnv;
                        points.RMSAcc = eachpoint.RMSAcc.ToString();
                        points.RMSVel = eachpoint.RMSVel.ToString();
                        points.RMSEnv = eachpoint.RMSEnv.ToString();
                        points.Temperature = eachpoint.Temperature.ToString();
                        pointlistall.Add(points);
                    }
                }
                if (pointlistall.Count > 0)
                {
                    return Ok(JsonConvert.SerializeObject(dataAll)); //không cần chuyển sang string
                }
                else
                {
                    string statusError = "No Data";
                    return NotFound(JsonConvert.SerializeObject(statusError));
                }
            }
            catch (Exception)
            {
                // Log or handle the exception
                return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
            }
        }
        [HttpGet("Sync/2")]
        public async Task<IActionResult> Sync2()
        {
            try
            {
                ConnectMongo ConnectDB = new ConnectMongo();
                List<Point_get> pointlistall = new List<Point_get>();
                List<Point> dataAll =  ConnectDB.GetSyncPoint2();
                if (dataAll != null)
                {
                    foreach (var eachpoint in dataAll)
                    {
                       Point_get points = new Point_get();
                       {
                           points.DateTime = ((DateTime)eachpoint.DateTime).ToString("yyyy/MM/dd HH:mm");
                           points.FFTAcc = eachpoint.FFTAcc;
                           points.FFTVel = eachpoint.FFTVel;
                           points.FFTEnv = eachpoint.FFTEnv;
                           points.RMSAcc = eachpoint.RMSAcc.ToString();
                           points.RMSVel = eachpoint.RMSVel.ToString();
                           points.RMSEnv = eachpoint.RMSEnv.ToString();
                           points.Temperature = eachpoint.Temperature.ToString();
                           pointlistall.Add(points);
                       }
                    }
                }
                if (pointlistall.Count > 0)
                {
                    return Ok(JsonConvert.SerializeObject(dataAll));
                }
                else
                {
                    string statusError = "No Data";
                    return NotFound(JsonConvert.SerializeObject(statusError));
                }
            }
            catch (Exception)
            {
                // Log or handle the exception
                return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
            }
        }
    }
}
