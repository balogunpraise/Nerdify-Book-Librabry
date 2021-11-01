using Models;
using System.Collections.Generic;

namespace LearningAuth.Models
{
    public class AddBookViewModel
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ImageUrl { get; set; }
        public string ISBN { get; set; }
        public int Pages { get; set; }
    }
}
