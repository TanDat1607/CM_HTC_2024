using CM_Local.ConnectDB;
using CM_Local.Entity;
using CM_Local.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;

namespace CM_Local.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class pmController : ControllerBase
    {
        [HttpGet("SyncCurrent")]
        public async Task<IActionResult> SyncCurrent()
        {
            try
            {
                ConnectMongo ConnectDB = new ConnectMongo();
                List<Current_get> currentlistall = new List<Current_get>();
                List<Current> dataAll =  ConnectDB.GetSyncCurrent();
                Logger.LogFile("Logs/log.txt", "aaa");
            //    DataTable dataAll = await Db.GetSyncCurrent();
                if (dataAll != null)
                {
                    foreach (var eachcurr in dataAll)
                    {
                        Current_get currents = new Current_get();
                        {
                            currents.DateTime = ((DateTime)eachcurr.DateTime).ToString("yyyy/MM/dd HH:mm");
                            currents.FFTI1 = eachcurr.FFTI1;
                            currents.FFTI2 = eachcurr.FFTI2;
                            currents.FFTI3 = eachcurr.FFTI3;
                            currents.RMSI1 = eachcurr.RMSI1.ToString(CultureInfo.InvariantCulture);
                            currents.RMSI2 = eachcurr.RMSI2.ToString(CultureInfo.InvariantCulture);
                            currents.RMSI3 = eachcurr.RMSI3.ToString(CultureInfo.InvariantCulture);
                            currents.Frequency = eachcurr.Frequency.ToString(CultureInfo.InvariantCulture);
                            currentlistall.Add(currents);
                        }
                    }
                }
                if (currentlistall.Count > 0)
                {
                    return Ok(JsonConvert.SerializeObject(currentlistall));
                }
                else
                {
                    string statusError = "No Data";
                    return NotFound(JsonConvert.SerializeObject(statusError));
                }
            }
            catch (Exception e)
            {
                // Log or handle the exception
                Logger.LogFile("Logs/log.txt", e.ToString());
                return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
            }
        }

        [HttpGet("SyncVoltage")]
        public async Task<IActionResult> SyncVoltage()
        {
            try
            {
                ConnectMongo ConnectDB = new ConnectMongo();
                List<Voltage_get> voltagelistall = new List<Voltage_get>();
                List<Voltage> dataAll =  ConnectDB.GetSyncVoltage();
              //  DataTable dataAll = await Db.GetSyncVoltage();
                foreach (var eachvolt in dataAll)
                {
                    Voltage_get voltages = new Voltage_get();
                    {
                        voltages.DateTime = ((DateTime)eachvolt.DateTime).ToString("yyyy/MM/dd HH:mm");
                        voltages.FFTU1 = eachvolt.FFTU1;
                        voltages.FFTU2 = eachvolt.FFTU2;
                        voltages.FFTU3 = eachvolt.FFTU3;
                        voltages.RMSU1 = eachvolt.RMSU1.ToString(CultureInfo.InvariantCulture);
                        voltages.RMSU1 = eachvolt.RMSU2.ToString(CultureInfo.InvariantCulture);
                        voltages.RMSU1 = eachvolt.RMSU3.ToString(CultureInfo.InvariantCulture);
                        voltages.Frequency = eachvolt.Frequency.ToString(CultureInfo.InvariantCulture);
                        voltagelistall.Add(voltages);
                    }
                }
                if (voltagelistall.Count > 0)
                {
                    return Ok(JsonConvert.SerializeObject(voltagelistall));
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
