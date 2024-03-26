using examApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace examApi.Services
{

    public interface IExamService
    {
        Task<IEnumerable<Exam>> GetAllExamsAsync();
        Task AddExamAsync(Exam exam);

        Task DeleteExamAsync(string examId);

        Task AddQuestionsToExamAsync(string examId, Question question);

        Task AddUserToExamAsync (string examId, string userId);
    }
    public class ExamService : IExamService
    {
        private readonly IMongoCollection<Exam> _examsCollection;
        public ExamService(IMongoClient mongoClient, string databaseName, string collectionName) { 
          
            var database = mongoClient.GetDatabase(databaseName);
           _examsCollection = database.GetCollection<Exam>(collectionName);
        
        }

        public async Task<IEnumerable<Exam>> GetAllExamsAsync()
        {
            return await _examsCollection.Find(Builders<Exam>.Filter.Empty).ToListAsync();
        }

        public async Task AddExamAsync(Exam exam)
        {
            await _examsCollection.InsertOneAsync(exam);
        }

        public async Task DeleteExamAsync(string examId)
        {
          
            var filter = Builders<Exam>.Filter.Eq(exam => exam.ID, examId);
            await _examsCollection.FindOneAndDeleteAsync(filter);
        }

        public async Task AddQuestionsToExamAsync(string examId, Question question)
        {

            
            var filter = Builders<Exam>.Filter.Eq(exam => exam.ID, examId);
            var exam = await _examsCollection.Find(filter).FirstOrDefaultAsync();

            if (exam == null)
            {
                throw new ArgumentException("Exam not found");
            }

            if (exam.questionCount <= exam.questions.Length + 1)
            {
                throw new InvalidOperationException("Questions cannot be added as the question count limit has been reached.");
            }

            var update = Builders<Exam>.Update.Push(exam => exam.questions, question);

            await _examsCollection.UpdateOneAsync(filter, update);
        }

        public async Task AddUserToExamAsync(string examId, string userId)
        {

            
            var filter = Builders<Exam>.Filter.Eq(exam => exam.ID, userId);
            var exam = await _examsCollection.Find(filter).FirstOrDefaultAsync() ?? throw new ArgumentException("Exam not found");

            var update = Builders<Exam>.Update.Push("participants", userId);
            await _examsCollection.UpdateOneAsync(filter, update);
        }



    }
}
