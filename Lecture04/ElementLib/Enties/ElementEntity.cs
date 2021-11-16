using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ElementLib.Enties
{
    public class ElementEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Symbol { get; set; }

        public int Z { get; set; }
        public string Name { get; set; }
        public float Mass { get; set; }

        public override string ToString() => $"{Z} [{Symbol}] {Name}";
    }
}
