using M6L1BooksAuthors.Core.Interfaces;
using M6L1BooksAuthors.Core.Models;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Data;
using M6L1BooksAuthors.Infrastructure.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace M6L1BooksAuthors.Core.Services
{
    public class BookService : IBookService
    {

        private readonly ApplicationContext _context;

        public BookService(ApplicationContext context)
        {
            _context = context;
        }

        public void AddProduct(BookAdd product)
        {
            try
            {
                using (_context)
                {
                    Book book = new Book { Description = product.Description, ReleaseYear = product.ReleaseYear, Title = product.Title };
                    for (int i = 0; i < product.Authors.Count; i++)
                    {
                        book.BooksAuthors.Add(new BookAuthor { Contribution = product.Authors[i].Contribution, Book = book, Author = new Author { Birthday = product.Authors[i].Birthday, FirstName = product.Authors[i].FirstName, LastName = product.Authors[i].LastName } });
                    }
                    _context.Add(book);
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
            }
        }

        public void UpdateProduct(BookPut product)
        {
            try
            {
                using (_context)
                {
                    Book book = _context.Books.Where(x => x.BookId == product.Id).FirstOrDefault();

                    if (book != null)
                    {
                        book.Title = product.Title;
                        book.Description = product.Description;
                        _context.Update(book);
                    }
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProduct(BookDelete product)
        {
            try
            {
                using (_context)
                {
                    Book book = _context.Books.Where(x => x.BookId == product.Id).FirstOrDefault();
                    _context.Remove(book);
                    _context.SaveChanges();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public Book GetBook(int id)
        {
            using (_context)
            {
                try
                {
                    Book book = _context.Books.Include(u => u.BooksAuthors).ThenInclude(i => i.Author).Where(i => i.BookId == id).FirstOrDefault();
                    if (book != null)
                    {
                        return book;
                    }

                    return null;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public List<Book> GetBooks()
        {
            using (_context)
            {
                try
                {
                    List<Book> books = _context.Books.Include(u => u.BooksAuthors).ThenInclude(i => i.Author).ToList();
                    return books;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
