using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string BooktypeId { get; set; }
        public BookType BookType { get; set; }
        public string Description { get; set; }
        public string AuthorId { get; set; }
        public Author Author { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
    }
}
