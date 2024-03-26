using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace examApi.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]

        public string ID { get; set; }

        public string username { get; set; }
        public string password { get; set; }

        public string role { get; set; }
    }
}
