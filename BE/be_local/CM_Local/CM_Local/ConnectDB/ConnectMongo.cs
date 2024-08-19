using System.Data;
using System.Data.SqlClient;
using CM_Local.Services;
using MongoDB.Bson;
using MongoDB.Driver;
using CM_Local.Entity;
namespace CM_Local.ConnectDB
{
    public class ConnectMongo
    {
        public readonly List<IMongoCollection<BsonDocument>> collection = new List<IMongoCollection<BsonDocument>>();
        private readonly string logFile = "Logs/log.txt";
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
                var database = client.GetDatabase("db_CM");
                // symbol collection
                _point1 = database.GetCollection<Point>("col_Point1");
                _point2 = database.GetCollection<Point>("col_Point2");
                _current = database.GetCollection<Current>("col_Current");
                _voltage = database.GetCollection<Voltage>("col_Voltage");
                Logger.LogFile(logFile, "Connect DB successed");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine("Connect DB failed");
                Logger.LogFile(logFile, ex.Message);
            }

        }

        // ----- READ COMMAND -----
        // get all data
        public List<Point> GetSyncPoint1()
        {
            var point1 = _point1.Find(_ => true).ToList();
            var deleteResult = _point1.DeleteMany(FilterDefinition<Point>.Empty);
            Logger.LogFile(logFile, "Get sync Point 1 succeeded");
            return point1;
        }
        public List<Point> GetSyncPoint2()
        {
            var point2 = _point2.Find(_ => true).ToList();
            var deleteResult = _point2.DeleteMany(FilterDefinition<Point>.Empty);
            Logger.LogFile(logFile, "Get sync Point 2 succeeded");
            return point2;
        }    
        public List<Current> GetSyncCurrent()
        {
            var current = _current.Find(_ => true).ToList();
            var deleteResult = _current.DeleteMany(FilterDefinition<Current>.Empty);
            Logger.LogFile(logFile, "Get sync Current succeeded");
            return current;
        }   
        public List<Voltage> GetSyncVoltage()
        {
            var voltage = _voltage.Find(_ => true).ToList();
            var deleteResult = _voltage.DeleteMany(FilterDefinition<Voltage>.Empty);
            Logger.LogFile(logFile, "Get sync Voltage succeeded");
            return voltage;
        }
        //Insert data
        public Point InsertPoint1(Point point)
        {
            _point1.InsertOne(point);
            Logger.LogFile(logFile, "Insert Point 1 succeeded");
            return point;
        }        
        public Point InsertPoint2(Point point)
        {
            _point2.InsertOne(point);
            Logger.LogFile(logFile, "Insert Point 2 succeeded");
            return point;
        }   
        public Current InsertCurrent(Current current)
        {
            _current.InsertOne(current);
            Logger.LogFile(logFile, "Insert Current succeeded");
            return current;
        }        
        public Voltage InsertVoltage(Voltage voltage)
        {
            _voltage.InsertOne(voltage);
            Logger.LogFile(logFile, "Insert Voltage succeeded");
            return voltage;
        }   
    }
}
