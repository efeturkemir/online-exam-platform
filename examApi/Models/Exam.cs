using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;

namespace examApi.Models
{
    [BsonIgnoreExtraElements]
    public class Exam
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]

        public string ID { get; set; }

        public string examType { get; set; }

        public string examName { get; set; }

        public int questionCount { get; set; }

        public string[] participants { get; set; }
        
        public Question[] questions { get; set; }

    }


    public class Question
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        [BsonRepresentation(BsonType.ObjectId)]

        public string ID { get; set; }
        public string questionText { get; set; }

        public string[] options { get; set; }

        public int answerIndex { get; set; }
    }
}
