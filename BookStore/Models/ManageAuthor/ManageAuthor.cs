using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ManageAuthor
{
    public class ManageAuthor : IManageAuthor
    {
        List<Author> Authors;
        public ManageAuthor()
        {
            Authors = new List<Author>()
            {
                new Author(){ Id = 1,fullName="Dan Brown"},
                new Author(){ Id = 2,fullName="John Green"},
                new Author(){ Id = 3,fullName="Nora Roberts"},
                new Author(){ Id = 4,fullName="Stephen King"},
            };
        }
        public void AddAuthor(Author author)
        {
            author.Id = Authors.Max(a => a.Id) + 1;
            Authors.Add(author);
        }

        public void DeleteAuthor(int authorId)
        {
            var author = GetAuthorById(authorId);
            Authors.Remove(author);
        }

        public void EditAuthor(Author author)
        {
            var Author = GetAuthorById(author.Id);
            Author.fullName = author.fullName;
        }

        public Author GetAuthorById(int authorId)
        {
            var author = Authors.SingleOrDefault(a => a.Id == authorId);
            return author;
        }

        public List<Author> GetAuthorByName(string searchName)
        {
            var authors = Authors.Where(a => a.fullName.StartsWith(searchName)).ToList();
            return authors;
        }

        public List<Author> GetAuthors()
        {
            return Authors;
        }
    }
}
