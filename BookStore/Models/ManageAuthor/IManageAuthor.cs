using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ManageAuthor
{
    public interface IManageAuthor
    {
        List<Author> GetAuthors();
        Author GetAuthorById(int authorId);
        List<Author> GetAuthorByName(string searchName);
        void AddAuthor(Author author);
        void EditAuthor(Author author);
        void DeleteAuthor(int authorId);
    }
}
