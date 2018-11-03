using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDbDemo.DataAccess;
using MongoDbDemo.Models;

namespace MongoDbDemo.Repositories
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAllBooks();
        IQueryable<Book> GetAllBooksAsQueryable();
        IEnumerable<Book> GetBooksByAuthor(string author);
        Book AddBook(Book book);
        bool AddBooks(IEnumerable<Book> book);
    }

    public class BooksRepository : IBooksRepository
    {
        private readonly IBooksDataService _dataService;

        public BooksRepository(IBooksDataService dataService)
        {
            _dataService = dataService;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            var books = _dataService.GetBooks().ToList();
            return books;
        }

        public IQueryable<Book> GetAllBooksAsQueryable()
        {
            return _dataService.GetBooks();
        }

        public IEnumerable<Book> GetBooksByAuthor(string author)
        {
            return _dataService.GetBooks().Where(b=>b.Author == author);
        }

        public Book AddBook(Book book)
        {

            var dto = _dataService.Create(book);
            return dto;
        }

        public bool AddBooks(IEnumerable<Book> books)
        {
            return _dataService.Create(books);
        }
    }
}
