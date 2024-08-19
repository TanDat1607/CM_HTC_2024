using System.Collections.Immutable;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Nodes;
using CM_Server.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using CM_Server.Entity;
using MongoDB.Driver.Linq;
using Newtonsoft.Json.Linq;

namespace CM_Server.ConnectDB
{
    
    public class ConnectMongo
    {
        public readonly List<IMongoCollection<BsonDocument>> collection = new List<IMongoCollection<BsonDocument>>();
        public const string LogFile = "Logs/log.txt";
        private readonly IMongoCollection<Point> _point1;
        private readonly IMongoCollection<Point> _point2;
        private readonly IMongoCollection<Current> _current;
        private readonly IMongoCollection<Voltage> _voltage;       
        
        public ConnectMongo()
        {
            // init connection
            try
            {
                var client = new MongoClient("mongodb://localhost:27017");
                var database = client.GetDatabase("db_CM_Server");
                // symbol collection
                _point1 = database.GetCollection<Point>("col_Point1");
                _point2 = database.GetCollection<Point>("col_Point2");
                _current = database.GetCollection<Current>("col_Current");
                _voltage = database.GetCollection<Voltage>("col_Voltage");
                Logger.LogFile(LogFile, "Connect DB successed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine("Connect DB failed");
                Logger.LogFile(LogFile, ex.Message);
            }

        }
        // ----- READ COMMAND -----
        // get all data
        public List<Point> GetDataPointDateTime(string point, DateTime startdate, DateTime enddate)
        {
          List<Point> data = new List<Point>();
          var builder = Builders<Point>.Filter;
          var filter = builder.Gte(r => r.DateTime, startdate) &
                       builder.Lte(r => r.DateTime, enddate);
          switch (int.Parse(point))
          {
              case 1: data = _point1.Find(filter).ToList(); break;
              case 2: data = _point2.Find(filter).ToList(); break;
          }
          return data;
        }
        public List<Point> GetDataRMS(string point, DateTime startdate, DateTime enddate)
        {
            List<Point> data = new List<Point>();
            List<Point> dataDB = new List<Point>();
            var builder = Builders<Point>.Filter;
          //  int count = 0;
            for (DateTime date = startdate; date <= enddate; date = date.AddHours(8))
            {
             var filter = builder.Eq(r => r.DateTime, date);
             Point eachDate = new Point();
             switch (int.Parse(point))
             {
                 case 1: dataDB = _point1.Find(filter).ToList(); break;
                 case 2: dataDB = _point2.Find(filter).ToList(); break;
             }
             if (dataDB.Count > 0)
             {
                 eachDate.RMSAcc = dataDB[0].RMSAcc;
                 eachDate.RMSVel = dataDB[0].RMSVel;
                 eachDate.RMSEnv = dataDB[0].RMSEnv;
             }
             else
             {
                 eachDate.RMSAcc = 0;
                 eachDate.RMSVel = 0;
                 eachDate.RMSEnv = 0;
             }
             data.Add(eachDate);
             //count++;
            }
          //  Logger.LogFile(LogFile, );
            return data;
        }
        public List<Current> GetCurrent(DateTime startdate, DateTime enddate)
        {
            List<Current> data = new List<Current>();
            var builder = Builders<Current>.Filter;
            var filter = builder.Gte(r => r.DateTime, startdate) &
                         builder.Lte(r => r.DateTime, enddate);
            data = _current.Find(filter).ToList();
            return data;
        }   
        public List<Voltage> GetVoltage(DateTime startdate, DateTime enddate)
        {
            List<Voltage> data = new List<Voltage>();
            var builder = Builders<Voltage>.Filter;
            var filter = builder.Gte(r => r.DateTime, startdate) &
                         builder.Lte(r => r.DateTime, enddate);
            data = _voltage.Find(filter).ToList();
            return data;
        }  
        //Insert data
        public Point InsertPoint1(Point point)
        {
            _point1.InsertOne(point);
            Logger.LogFile(LogFile, "Insert Point 1 succeeded");
            return point;
        }        
        public Point InsertPoint2(Point point)
        {
            _point2.InsertOne(point);
            Logger.LogFile(LogFile, "Insert Point 2 succeeded");
            return point;
        }   
        public Current InsertCurrent(Current current)
        {
            _current.InsertOne(current);
            Logger.LogFile(LogFile, "Insert Current succeeded");
            return current;
        }        
        public Voltage InsertVoltage(Voltage voltage)
        {
            _voltage.InsertOne(voltage);
            Logger.LogFile(LogFile, "Insert Voltage succeeded");
            return voltage;
        }   
    }
}
