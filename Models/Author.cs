using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Author : BaseEntity
    {
        public string Name { get; set; }
        public string About { get; set; }
        public int NumberOfBooks { get; set; }
        public string ImageUrl { get; set; }
    }
}
