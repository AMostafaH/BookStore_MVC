using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ManageBook
{
    public interface IManageBook
    {
        List<Book> GetBooks();
        Book GetBookById(int bookId);
        List<Book> GetBookByName(string searchName);
        void AddBook(Book book);
        //void EditBook(int id, Book book);
        void EditBook(int id, Book book);
        void DeleteBook(int bookId);
    }
}
