using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CM_Server.Entity
{
    public class Voltage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonElement("datetime")]
        public DateTime DateTime { get; set; }
        
        [BsonElement("FFTU1")]
        public string FFTU1 { get; set; }
        
        [BsonElement("FFTU2")]
        public string FFTU2 { get; set; }
        
        [BsonElement("FFTU3")]
        public string FFTU3 { get; set; }
        
        [BsonElement("RMSU1")]
        public double RMSU1 { get; set; }
        
        [BsonElement("RMSU2")]
        public double RMSU2 { get; set; }
        
        [BsonElement("RMSU3")]
        public double RMSU3 { get; set; }
        
        [BsonElement("frequency")]
        public float Frequency { get; set; }
    }
}
