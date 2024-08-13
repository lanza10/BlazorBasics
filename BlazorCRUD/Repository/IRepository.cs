using BlazorCRUD.Models;

namespace BlazorCRUD.Repository
{
    public interface IRepository
    {
        public Task<List<Book>> GetBooks();
        public Task<Book> GetBookById(int bookId);
        public Task<Book> CreateBook(Book book);
        public Task<Book> UpdateBook(int bookId, Book book);
        public Task DeleteBook(int bookId);
    }
}
