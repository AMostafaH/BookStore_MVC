using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ManageBook
{
    public class ManageBookDB : IManageBook
    {
        private readonly BookStoreDbContext context;

        public ManageBookDB(BookStoreDbContext _context)
        {
            context = _context;
        }
        public void AddBook(Book book)
        {
            context.Books.Add(book);
            context.SaveChanges();
        }

        public void DeleteBook(int bookId)
        {
            var Book = GetBookById(bookId);
            context.Books.Remove(Book);
            context.SaveChanges();
        }

        //public void EditBook(int id, Book book)
        public void EditBook(int id, Book book)
        {
            var Book = GetBookById(id);
            if(Book != null)
            {
                Book.Title = book.Title;
                Book.Description = book.Description;
                Book.Author = book.Author;
                Book.ImageURL = book.ImageURL;

                context.SaveChanges();
            }  
        }

        public Book GetBookById(int bookId)
        {
            var book = context.Books.Include(a=>a.Author).SingleOrDefault(b => b.Id == bookId);
            return book;
        }

        public List<Book> GetBookByName(string searchName)
        {
            var books = context.Books.Where(b => b.Title.StartsWith(searchName)).ToList();
            return books;
        }

        public List<Book> GetBooks()
        {
            return context.Books.Include(a=>a.Author).ToList();
        }
    }
}
