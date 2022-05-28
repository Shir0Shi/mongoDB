using System;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Management.Automation;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using System.Linq;

namespace lab12
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            FindMovies(client).GetAwaiter().GetResult();
            Console.WriteLine();
            FindMoviesShort(client).GetAwaiter().GetResult();
            Console.ReadLine();
        }
        private static async Task FindMovies(MongoClient client)
        {
            var database = client.GetDatabase("movie");
            var collection = database.GetCollection<BsonDocument>("movie");
            var filter = new BsonDocument();
            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var movie = cursor.Current;
                    foreach (var doc in movie)
                    {
                        Console.WriteLine(doc);
                    }
                }
            }
        }
        private static async Task FindMoviesShort(MongoClient client)
        {
            var database = client.GetDatabase("movie");
            var collection = database.GetCollection<BsonDocument>("movie");
            var filter = new BsonDocument("name", "The Dark Knight");
            var people = await collection.Find(filter).ToListAsync();
            foreach (var doc in people)
            {
                Console.WriteLine(doc);
            }
        }

    }
}
