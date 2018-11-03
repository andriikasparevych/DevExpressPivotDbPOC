using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDbDemo.Models;

namespace MongoDbDemo.DataAccess
{
    public interface IBooksDataService
    {
        IQueryable<Book> GetBooks();
        Book Create(Book dto);
        bool Create(IEnumerable<Book> dto);
    }
}
