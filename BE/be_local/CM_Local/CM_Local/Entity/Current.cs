using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace CM_Local.Entity
{
    public class Current
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("datetime")]
        public DateTime? DateTime { get; set; }
        
        [BsonElement("FFTI1")]
        public string? FFTI1 { get; set; }
        
        [BsonElement("FFTI2")]
        public string? FFTI2 { get; set; }
        
        [BsonElement("FFTI3")]
        public string? FFTI3 { get; set; }
        
        [BsonElement("RMSI1")]
        public double RMSI1 { get; set; }
        
        [BsonElement("RMSI2")]
        public double RMSI2 { get; set; }
        
        [BsonElement("RMSI3")]
        public double RMSI3 { get; set; }
        
        [BsonElement("frequency")]
        public float Frequency { get; set; }
    }
}
