using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    
    public class BookRepository
    {
        private readonly DatabaseContext _context;
        public BookRepository(DatabaseContext context)
        {
            _context = context;
        }



        

        //Get Requests    

        public async Task<ICollection<Book>> GetBooks()
        {
            var books = await _context.Books
                .Include(x=>x.Author)
                .Include(x=>x.BookType)
                .Include(x => x.Reviews)
                .ToListAsync();
            return books;
        }


        public async Task<Book> GetBook(string title)
        {
            Book book = await _context.Books.Where(x => x.Title == title)
                .Include(x =>x.BookType)
                .Include(x =>x.Author)
                .Include(x =>x.Reviews)
                .SingleOrDefaultAsync();
            return book;
        }


        public async Task<ICollection<Book>> GetBooksByAuthor(string author)
        {
            var books = await _context.Books.Where(x => x.Author.Name == author)
                .Include(x=>x.BookType).Include(x => x.Reviews).ToListAsync();
            return books;
        }


        public async Task<ICollection<Book>> GetBooksByType(string type)
        {
            var books = await _context.Books.Where(x => x.BookType.Genre == type)
                .Include(x => x.Author).Include(x => x.Reviews).ToListAsync();
            return books;
        }





        // ADD REQUESTS
        public async Task AddBook(Book bookModel)
        {
            await _context.AddAsync(bookModel);
        }


        // REMOVE REQUEST

        public async Task DeleteBook(string id)
        {
            var book = await _context.Books.Where(x => x.Id == id).FirstOrDefaultAsync();
            if(book is not null)
            {
                 _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

    }

}
