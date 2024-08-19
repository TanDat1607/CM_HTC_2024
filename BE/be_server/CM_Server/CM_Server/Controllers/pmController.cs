using CM_Server.ConnectDB;
using CM_Server.Entity;
using CM_Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;

namespace CM_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PmController : ControllerBase
    {
        readonly ConnectMongo _connectDb;
        private const string Format = "yyyy-MM-dd HH:mm";
        private const string LogPath = ConnectDB.ConnectMongo.LogFile;
        //======================
        [HttpGet("FFTI1/{datetime}")]
        public Task<IActionResult> FFTI1(string datetime)
        {
            DateTime parsedDateTime = DateTime.ParseExact(datetime, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetCurrent(parsedDateTime,parsedDateTime);
                if (dataAll.Count > 0)
                {
                    string resultFftI1 = dataAll[0].FFTI1;
                    Double[] result = Array.ConvertAll(resultFftI1.Split(','), Double.Parse);
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(result)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get FFT I1 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //======================
        [HttpGet("FFTI2/{datetime}")]
        public Task<IActionResult> FFTI2(string datetime)
        {
            DateTime parsedDateTime = DateTime.ParseExact(datetime, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetCurrent(parsedDateTime,parsedDateTime);
                if (dataAll.Count > 0)
                {
                    string resultFftI2 = dataAll[0].FFTI2;
                    Double[] result = Array.ConvertAll(resultFftI2.Split(','), Double.Parse);
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(result)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get FFT I2 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //======================
        [HttpGet("FFTI3/{datetime}")]
        public Task<IActionResult> FFTI3(string datetime)
        {
            DateTime parsedDateTime = DateTime.ParseExact(datetime, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetCurrent(parsedDateTime,parsedDateTime);
                if (dataAll.Count > 0)
                {
                    string resultFftI3 = dataAll[0].FFTI3;
                    Double[] result = Array.ConvertAll(resultFftI3.Split(','), Double.Parse);
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(result)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get FFT I3 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //==================
        [HttpGet("RMSI1/{startdate}/{enddate}")]
        public Task<IActionResult> RMSI1(string startdate,string enddate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startdate, Format, CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(enddate, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetCurrent(parsedStartDate,parsedEndDate);
                if (dataAll.Count > 0)
                {
                    string[] result = new string[dataAll.Count];
                    for (int i = 0; i < dataAll.Count; i++)
                    { result[i] = dataAll[i].RMSI1.ToString(CultureInfo.InvariantCulture); }
                    Double[] vs = result.Select(double.Parse).ToArray();
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(vs)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get RMS I1 datetime: " + ex);
               return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //==================
        [HttpGet("RMSI2/{startdate}/{enddate}")]
        public Task<IActionResult> RMSI2(string startdate, string enddate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startdate, Format, CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(enddate, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetCurrent(parsedStartDate,parsedEndDate);
                if (dataAll.Count > 0)
                {
                    string[] result = new string[dataAll.Count];
                    for (int i = 0; i < dataAll.Count; i++)
                    { result[i] = dataAll[i].RMSI2.ToString(CultureInfo.InvariantCulture); }
                    Double[] vs = result.Select(double.Parse).ToArray();
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(vs)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data")));}
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get RMS I2 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //==================
        [HttpGet("RMSI3/{startdate}/{enddate}")]
        public Task<IActionResult> RMSI3(string startdate, string enddate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startdate, Format, CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(enddate, Format, CultureInfo.InvariantCulture); 
            try
            {
                var dataAll = _connectDb.GetCurrent(parsedStartDate,parsedEndDate);
                if (dataAll.Count > 0)
                {
                    string[] result = new string[dataAll.Count];
                    for (int i = 0; i < dataAll.Count; i++)
                    { result[i] = dataAll[i].RMSI3.ToString(CultureInfo.InvariantCulture); }
                    Double[] vs = result.Select(double.Parse).ToArray();
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(vs)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get RMS I3 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }

        //===============================Voltage================================================
        //======================
        [HttpGet("FFTU1/{datetime}")]
        public Task<IActionResult> FFTU1(string datetime)
        {
            DateTime parsedDateTime = DateTime.ParseExact(datetime, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetVoltage(parsedDateTime,parsedDateTime);
                if (dataAll.Count > 0)
                {
                    string resultFftU1 = dataAll[0].FFTU1;
                    Double[] result = Array.ConvertAll(resultFftU1.Split(','), Double.Parse);
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(result)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get FFT U1 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //======================
        [HttpGet("FFTU2/{datetime}")]
        public Task<IActionResult> FFTU2(string datetime)
        {
            DateTime parsedDateTime = DateTime.ParseExact(datetime, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetVoltage(parsedDateTime,parsedDateTime);
                if (dataAll.Count > 0)
                {
                    string resultFftU2 = dataAll[0].FFTU2;
                    Double[] result = Array.ConvertAll(resultFftU2.Split(','), Double.Parse);
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(result)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get FFT U2 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //======================
        [HttpGet("FFTU3/{datetime}")]
        public Task<IActionResult> FFTU3(string datetime)
        {
            DateTime parsedDateTime = DateTime.ParseExact(datetime, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetVoltage(parsedDateTime,parsedDateTime);
                if (dataAll.Count > 0)
                {
                    string resultFftU3 = dataAll[0].FFTU3;
                    Double[] result = Array.ConvertAll(resultFftU3.Split(','), Double.Parse);
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(result)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get FFT U3 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //==================
        [HttpGet("RMSU1/{startdate}/{enddate}")]
        public Task<IActionResult> RMSU1(string startdate, string enddate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startdate, Format, CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(enddate, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetVoltage(parsedStartDate,parsedEndDate);
                if (dataAll.Count > 0)
                {
                    string[] result = new string[dataAll.Count];
                    for (int i = 0; i < dataAll.Count; i++)
                    { result[i] = dataAll[i].RMSU1.ToString(CultureInfo.InvariantCulture); }
                    Double[] vs = result.Select(double.Parse).ToArray();
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(vs)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get RMS U1 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //==================
        [HttpGet("RMSU2/{startdate}/{enddate}")]
        public Task<IActionResult> RMSU2(string startdate, string enddate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startdate, Format, CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(enddate, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetVoltage(parsedStartDate,parsedEndDate);
                if (dataAll.Count > 0)
                {
                    string[] result = new string[dataAll.Count];
                    for (int i = 0; i < dataAll.Count; i++)
                    { result[i] = dataAll[i].RMSU1.ToString(CultureInfo.InvariantCulture); }
                    Double[] vs = result.Select(double.Parse).ToArray();
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(vs)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get RMS U2 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
        //=================================
        //==================
        [HttpGet("RMSU3/{startdate}/{enddate}")]
        public Task<IActionResult> RMSU3(string startdate, string enddate)
        {
            DateTime parsedStartDate = DateTime.ParseExact(startdate, Format, CultureInfo.InvariantCulture);
            DateTime parsedEndDate = DateTime.ParseExact(enddate, Format, CultureInfo.InvariantCulture);
            try
            {
                var dataAll = _connectDb.GetVoltage(parsedStartDate,parsedEndDate);
                if (dataAll.Count > 0)
                {
                    string[] result = new string[dataAll.Count];
                    for (int i = 0; i < dataAll.Count; i++)
                    { result[i] = dataAll[i].RMSU1.ToString(CultureInfo.InvariantCulture); }
                    Double[] vs = result.Select(double.Parse).ToArray();
                    return Task.FromResult<IActionResult>(Ok(JsonConvert.SerializeObject(vs)));
                }
                { return Task.FromResult<IActionResult>(NotFound(JsonConvert.SerializeObject("No data"))); }
            }
            catch (Exception ex)
            {
                Logger.LogFile(LogPath, "Exception get RMS U3 datetime: " + ex);
                return Task.FromResult<IActionResult>(StatusCode(500, JsonConvert.SerializeObject("Internal Server Error")));
            }
        }
    }
}
