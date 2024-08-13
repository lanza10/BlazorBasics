using BlazorCRUD.Data;
using BlazorCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorCRUD.Repository
{
    public class Repository:IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Book>> GetBooks()
        {
            return await _context.Book.ToListAsync();
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var dbBook = await _context.Book.FindAsync(bookId);
            return dbBook ?? new Book();
        }

        public async Task<Book> CreateBook(Book book)
        {
            if (book == null)
            {
                return new Book();
            }
            book.CreationDate = DateTime.Now;
            await _context.Book.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> UpdateBook(int bookId, Book book)
        {
            var dbBook = await GetBookById(bookId);
            dbBook.Title = book.Title;
            dbBook.Description = book.Description;
            dbBook.Author = book.Author;
            dbBook.Pages = book.Pages;
            dbBook.Price = book.Price;

            await _context.SaveChangesAsync();
            return dbBook;
        }

        public async Task DeleteBook(int bookId)
        {
            var dbBook = await GetBookById(bookId);
            _context.Remove(dbBook);
            await _context.SaveChangesAsync();
        }
    }
}
