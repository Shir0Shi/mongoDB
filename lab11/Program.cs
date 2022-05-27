using System;
using System.Configuration;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Management.Automation;
using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Bson.Serialization;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Conventions;

namespace ConsoleApp1
{
    class Movie
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public List<string> Country { get; set; }
        public List<Genre> Genre { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors { get; set; }
        public int Year { get; set; }
        public List<string> Type { get; set; }
    }
    class Genre
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    class Director
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Biografy { get; set; }
    }
    class Actor
    {
        public Object Id { get; set; }
        public string Name { get; set; }
        public string Biografy { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            

            SaveDocs(client).GetAwaiter().GetResult();

            SaveManyDocs(client).GetAwaiter().GetResult();

            Console.ReadLine();
        }
        private static async Task SaveDocs(MongoClient client)
        {
            var database = client.GetDatabase("movie");
            var collection = database.GetCollection<BsonDocument>("director");
            BsonDocument director1 = new BsonDocument
            {
                {"name", "Steven Spielberg"},
                {"biografy", "is an American film director, producer, and screenwriter."}
                
            };
            await collection.InsertOneAsync(director1);
        }
        private static async Task SaveManyDocs(MongoClient client)
        {            
            var database = client.GetDatabase("movie");
            var collection = database.GetCollection<BsonDocument>("director");
            BsonDocument director2 = new BsonDocument
            {
                {"name", "Akira Kurosawa"},
                {"biografy", "was a Japanese filmmaker and painter who directed thirty films in a career spanning over five decades."}

            };
            BsonDocument director3 = new BsonDocument
            {
                {"name", "Alfred Hitchcock"},
                {"biografy", "was an English filmmaker widely regarded as one of the most influential figures in the history of cinema."}
            };
            await collection.InsertManyAsync(new[] { director2, director3 });
        }
        private static async Task SaveObjectDocs(MongoClient client)
        {
            var database = client.GetDatabase("movie");
            var collection = database.GetCollection<Movie>("movie");
            Movie movie1 = new Movie
            {
                Name = "Jack",
                Age = 29,
                Languages = new List<string> { "english", "german" },
                Company = new Company
                {
                    Name = "Google"
                }
            };
            await collection.InsertOneAsync(movie1);
            Console.WriteLine(movie1.Id);
        }

    }
}
