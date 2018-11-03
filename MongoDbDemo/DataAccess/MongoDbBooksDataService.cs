using System.Collections.Generic;
using System.Linq;
using MongoDbDemo.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace MongoDbDemo.DataAccess
{
    public class MongoDbBooksDataService : IBooksDataService
    {
        private readonly MongoClient _client;

        public MongoDbBooksDataService()
        {
            BsonClassMap.RegisterClassMap<Book>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId))
                    .SetIdGenerator(StringObjectIdGenerator.Instance);

            });

            _client = new MongoClient("mongodb://localhost:27017");

        }
        public IQueryable<Book> GetBooks()
        {
            var db = _client.GetDatabase("booksStore");
            return db.GetCollection<Book>("book").AsQueryable();
        }

        public Book Create(Book dto)
        {
            var db = _client.GetDatabase("booksStore");
            db.GetCollection<Book>("book").InsertOne(dto);
            return dto;
        }

        public bool Create(IEnumerable<Book> dto)
        {
            var db = _client.GetDatabase("booksStore");
            db.GetCollection<Book>("book").InsertMany(dto);
            return true;
        }

    }
}