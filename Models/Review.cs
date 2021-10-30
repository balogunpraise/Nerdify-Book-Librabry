using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Review : BaseEntity
    {
        public int Rating { get; set; }
        public string BookId { get; set; }
        public Book Book { get; set; }
        public string UserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
