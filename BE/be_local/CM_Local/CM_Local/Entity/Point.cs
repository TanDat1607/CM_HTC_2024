using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CM_Local.Entity
{
    public class Point
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("datetime")]
        public DateTime? DateTime { get; set; }
        
        [BsonElement("FFTAcc")]
        public string? FFTAcc { get; set; }
        
        [BsonElement("FFTVel")]
        public string? FFTVel { get; set; }
        
        [BsonElement("FFTEnv")]
        public string? FFTEnv { get; set; }
        
        [BsonElement("RMSAcc")]
        public double RMSAcc { get; set; }
        
        [BsonElement("RMSVel")]
        public double RMSVel { get; set; }
        
        [BsonElement("RMSEnv")]
        public double RMSEnv { get; set; }
        
        [BsonElement("Temperature")]
        public float Temperature { get; set; }
    }
}
