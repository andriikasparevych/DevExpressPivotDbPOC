using System;
using System.Linq;
using GalaSoft.MvvmLight.Ioc;
using MongoDbDemo.DataAccess;
using MongoDbDemo.Models;
using MongoDbDemo.Repositories;
using MongoDbDemo.ViewModel;
using NUnit.Framework;

namespace MongoDbDemo.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        private IBooksRepository _repo;

        [SetUp]
        public void SetUp()
        {
            SimpleIoc.Default.Register<IBooksDataService, MongoDbBooksDataService>();
            SimpleIoc.Default.Register<IBooksRepository, BooksRepository>();

            _repo = SimpleIoc.Default.GetInstance<IBooksRepository>();
        }

        
        [TestCase("Nathaniel Hawthorne", 500000)]
        [TestCase("Alice Walker", 500000)]
        [TestCase("Kurt Vonnegut", 500000)]
        public void CreateBooksForAuthor(string author, int amount)
        {
            var rand = new Random(DateTime.Now.Millisecond);

            for (var i = 0; i < amount; i++)
            {
                _repo.AddBook(new Book()
                {
                    Author = author,
                    Name = $"{author}_Book{i}",
                    AvailableAmount = rand.Next(1000),
                    Price = rand.Next(20,100)
                });
            }

            var count = _repo.GetBooksByAuthor(author).Count();

            Assert.AreEqual(amount, count);
        }
    }
}
