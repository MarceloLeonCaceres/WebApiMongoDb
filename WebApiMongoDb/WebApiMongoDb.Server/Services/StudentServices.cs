using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiMongoDb.Server.Data;
using WebApiMongoDb.Server.Models;

namespace WebApiMongoDb.Server.Services
{
    public class StudentServices
    {
        private readonly IMongoCollection<Student> _studenCollection;

        public StudentServices(IOptions<DatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(settings.Value.Connection);
            var mongoDb = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _studenCollection = mongoDb.GetCollection<Student>(settings.Value.CollectionName);
        }

        // get all students
        public async Task<List<Student>> GetAsync()
        {
            return await _studenCollection.Find(_ => true).ToListAsync();
        }

        // get student by id
        public async Task<Student> GetAsync(string id) =>
            await _studenCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        // add new student
        public async Task CreateAsync(Student newStudent) =>
            await _studenCollection.InsertOneAsync(newStudent);


        // update a student
        public async Task UpdateAsync(string id, Student updateStudent) =>
            await _studenCollection.ReplaceOneAsync(x => x.Id == id, updateStudent);

        // delete student
        public async Task RemoveAsync(string id) =>
            await _studenCollection.DeleteOneAsync(x => x.Id == id);
    }
}
