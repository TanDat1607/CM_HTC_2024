using CM_Server.ConnectDB;
using CM_Server.Entity;
using CM_Server.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Globalization;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace CM_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class cmController : ControllerBase
    {
        readonly ConnectMongo _connectDb = new ConnectMongo();
        TimeZoneInfo _localTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        private const string FormatDateTime = "yyyy-MM-dd HH:mm";
        private const string FormatDate = "yyyy-MM-dd";
        private const string LogPath = ConnectMongo.LogFile;
     
        [HttpGet("Acc/{point}/{datetime}")]
        public async Task<IActionResult> Acc(string point, string datetime)
        {
            string[] dateTimeParts = datetime.Split(' ');
            if (dateTimeParts[1].Length == 4)
            { datetime = dateTimeParts[0] + " " + "0" + dateTimeParts[1];}
            DateTime parsedDateTime = DateTime.ParseExact(datetime, FormatDateTime, CultureInfo.InvariantCulture);
            try
            {
                List<Point> dataAll = new List<Point>();
                dataAll = _connectDb.GetDataPointDateTime(point,parsedDateTime,parsedDateTime);
                if ( dataAll.Count > 0)
                {
                    string resultacc = dataAll[0].FFTAcc;
                    Double[] result = Array.ConvertAll(resultacc.Split(','), Double.Parse);
                    return Ok(JsonConvert.SerializeObject(result));
                }
                { return NotFound(JsonConvert.SerializeObject("No data")); }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                Logger.LogFile( LogPath, "Exception get acc datetime " + ex);
                return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
            }
        } 
    //==================
    [HttpGet("Vel/{point}/{datetime}")]
    public async Task<IActionResult> Vel(string point, string datetime)
    {
        string[] dateTimeParts = datetime.Split(' ');
        if (dateTimeParts[1].Length == 4)
        { datetime = dateTimeParts[0] + " " + "0" + dateTimeParts[1];}
        DateTime parsedDateTime = DateTime.ParseExact(datetime, FormatDateTime, CultureInfo.InvariantCulture);
        try
        {
            List<Point> dataAll = new List<Point>();
            dataAll = _connectDb.GetDataPointDateTime(point,parsedDateTime,parsedDateTime);
            if ( dataAll.Count > 0)
            {
                string resultacc = dataAll[0].FFTVel;
                Double[] result = Array.ConvertAll(resultacc.Split(','), Double.Parse);
                return Ok(JsonConvert.SerializeObject(result));
            }
            { return NotFound(JsonConvert.SerializeObject("No data")); }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Logger.LogFile(LogPath, "Exception get vel datetime " + ex);
            return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
        }
    }
    //=================================
    [HttpGet("Env/{point}/{datetime}")]
    public async Task<IActionResult> Env(string point, string datetime)
    {
        string[] dateTimeParts = datetime.Split(' ');
        if (dateTimeParts[1].Length == 4)
        { datetime = dateTimeParts[0] + " " + "0" + dateTimeParts[1];}
        DateTime parsedDateTime = DateTime.ParseExact(datetime, FormatDateTime, CultureInfo.InvariantCulture);
        try
        {
            List<Point> dataAll = new List<Point>();
            dataAll = _connectDb.GetDataPointDateTime(point,parsedDateTime,parsedDateTime);
            if ( dataAll.Count > 0)
            {
                string resultacc = dataAll[0].FFTEnv;
                Double[] result = Array.ConvertAll(resultacc.Split(','), Double.Parse);
                return Ok(JsonConvert.SerializeObject(result));
            }
            { return NotFound(JsonConvert.SerializeObject("No data")); }
        }
        catch (Exception ex)
        {
            // Log or handle the exception
            Logger.LogFile(LogPath, "Exception get env datetime " + ex);
            return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
        }
    }
    //=================================
    [HttpGet("RMSAcc/{point}/{startdate}/{enddate}")]
    public async Task<IActionResult> RMSAcc(string point ,string startdate,string enddate)
    {
        DateTime parsedStartDate = DateTime.ParseExact(startdate, FormatDate, CultureInfo.InvariantCulture).AddHours(6);
        DateTime parsedEndDate = DateTime.ParseExact(enddate, FormatDate, CultureInfo.InvariantCulture).AddHours(22);
        try
        {
            List<Point> dataAll = new List<Point>();
            dataAll = _connectDb.GetDataRMS(point,parsedStartDate,parsedEndDate);
            if (dataAll.Count > 0)
            {
                string[] result = new string[dataAll.Count];
                for (int i = 0; i < dataAll.Count; i++)
                {
                    result[i] = dataAll[i].RMSAcc.ToString(CultureInfo.InvariantCulture);
                }
                Double[] vs = result.Select(double.Parse).ToArray();
                return Ok(JsonConvert.SerializeObject(vs));
            }
            { return NotFound(JsonConvert.SerializeObject("No data")); }
        }
        catch (Exception ex)
        {
            //Log or handle the exception
            Logger.LogFile(LogPath, "Exception get rms acc datetime: " + ex);
            return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
        }
    }
    //=================================
    [HttpGet("RMSVel/{point}/{startdate}/{enddate}")]
    public async Task<IActionResult> RMSVel(string point, string startdate, string enddate)
    {
        DateTime parsedStartDate = DateTime.ParseExact(startdate, FormatDate, CultureInfo.InvariantCulture).AddHours(6);
        DateTime parsedEndDate = DateTime.ParseExact(enddate, FormatDate, CultureInfo.InvariantCulture).AddHours(22);   
        try
        {
            List<Point> dataAll = new List<Point>();
            dataAll = _connectDb.GetDataRMS(point,parsedStartDate,parsedEndDate);
            if (dataAll.Count > 0)
            {
                string[] result = new string[dataAll.Count];
                for (int i = 0; i < dataAll.Count; i++)
                {
                    result[i] = dataAll[i].RMSVel.ToString(CultureInfo.InvariantCulture);
                }
                Double[] vs = result.Select(double.Parse).ToArray();
                return Ok(JsonConvert.SerializeObject(vs));
            }
            { return NotFound(JsonConvert.SerializeObject("No da ta")); }
        }
        catch (Exception ex)
        {
            //Log or handle the exception
            Logger.LogFile(LogPath, "Exception get rms vel datetime: " + ex);
            return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
        }
    }
    //=================================
    [HttpGet("RMSEnv/{point}/{startdate}/{enddate}")]
    public async Task<IActionResult> RMSEnv(string point, string startdate, string enddate)
    {
        DateTime parsedStartDate = DateTime.ParseExact(startdate, FormatDate, CultureInfo.InvariantCulture).AddHours(6);
        DateTime parsedEndDate = DateTime.ParseExact(enddate, FormatDate, CultureInfo.InvariantCulture).AddHours(22);
    
        try
        {
            List<Point> dataAll = new List<Point>();
            dataAll = _connectDb.GetDataRMS(point,parsedStartDate,parsedEndDate);
            if (dataAll.Count > 0)
            {
                string[] result = new string[dataAll.Count];
                for (int i = 0; i < dataAll.Count; i++)
                {
                    result[i] = dataAll[i].RMSEnv.ToString(CultureInfo.InvariantCulture);
                }
                Double[] vs = result.Select(double.Parse).ToArray();
                return Ok(JsonConvert.SerializeObject(vs));
            }
            { return NotFound(JsonConvert.SerializeObject("No data")); }
        }
        catch (Exception ex)
        {
            //Log or handle the exception
            Logger.LogFile(LogPath, "Exception get rms env datetime: " + ex);
            return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
        }
    }
    //=================================
    [HttpGet("Temperature/{point}/{startdate}/{enddate}")]
    public async Task<IActionResult> Temperature(string point, string startdate, string enddate)
    {
        DateTime parsedStartDate = DateTime.ParseExact(startdate, FormatDate, CultureInfo.InvariantCulture);
        DateTime parsedEndDate = DateTime.ParseExact(enddate, FormatDate, CultureInfo.InvariantCulture);   
        try
        {
            List<Point> dataAll = new List<Point>();
            dataAll = _connectDb.GetDataPointDateTime(point,parsedStartDate,parsedEndDate);
            if (dataAll.Count > 0)
            {
                string[] result = new string[dataAll.Count];
                for (int i = 0; i < dataAll.Count; i++)
                {
                    result[i] = dataAll[i].Temperature.ToString(CultureInfo.InvariantCulture);
                }
                Double[] vs = result.Select(double.Parse).ToArray();
                return Ok(JsonConvert.SerializeObject(vs));
            }
            { return NotFound(JsonConvert.SerializeObject("No data")); }
        }
        catch (Exception ex)
        {
            //Log or handle the exception
            Logger.LogFile(LogPath, "Exception get temp datetime: " + ex);
            return StatusCode(500, JsonConvert.SerializeObject("Internal Server Error"));
        }
    }
     }
}
