using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ManageBook
{
    public class ManageBook : IManageBook
    {
        List<Book> Books;
        public ManageBook()
        {
            Books = new List<Book>()
            {
                new Book(){ Id = 1, Title = "book1", Description = "nothing1", Author = new Author(){ }},
                new Book(){ Id = 2, Title = "book2", Description = "nothing2", Author = new Author(){ }},
                new Book(){ Id = 3, Title = "book3", Description = "nothing3", Author = new Author(){ }},
                new Book(){ Id = 4, Title = "book4", Description = "nothing4", Author = new Author(){ }},
            };
        }
        public void AddBook(Book book)
        {
            book.Id = Books.Max(b => b.Id) + 1;
            Books.Add(book);
        }

        public void DeleteBook(int bookId)
        {
            var Book = GetBookById(bookId);
            Books.Remove(Book);
        }

        //public void EditBook(int id, Book book)
        public void EditBook(int id, Book book)
        {
            var Book = GetBookById(id);
            Book.Title = book.Title;
            Book.Description = book.Description;
            Book.Author = book.Author;
            Book.ImageURL = book.ImageURL;
        }

        public Book GetBookById(int bookId)
        {
            var book = Books.SingleOrDefault(b => b.Id == bookId);
            return book;
        }

        public List<Book> GetBookByName(string searchName)
        {
            var books = Books.Where(b => b.Title.StartsWith(searchName)).ToList();
            return books;
        }

        public List<Book> GetBooks()
        {
            return Books;
        }
    }
}
