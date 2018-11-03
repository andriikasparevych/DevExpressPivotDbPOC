using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDbDemo.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public int AvailableAmount { get; set; }
        public int Price { get; set; }
    }
}
