using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CM_Server.Entity
{
    public class RMS
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("datetime")]
        public DateTime DateTime { get; set; }
        
        [BsonElement("RMSAcc")]
        public double RMSAcc { get; set; }
        
        [BsonElement("RMSVel")]
        public double RMSVel { get; set; }
        
        [BsonElement("RMSEnv")]
        public double RMSEnv { get; set; }
        
    }
}
