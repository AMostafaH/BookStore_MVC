using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models.ManageAuthor
{
    public class ManageAuthorDB : IManageAuthor
    {
        private readonly BookStoreDbContext context;
        public ManageAuthorDB(BookStoreDbContext _context)
        {
            context = _context;
        }
        public void AddAuthor(Author author)
        {
            context.Authors.Add(author);
            context.SaveChanges();
        }

        public void DeleteAuthor(int authorId)
        {
            var author = GetAuthorById(authorId);
            context.Authors.Remove(author);
            context.SaveChanges();
        }

        public void EditAuthor(Author author)
        {
            var Author = GetAuthorById(author.Id);
            if (Author != null)
            {
                Author.fullName = author.fullName;
                context.SaveChanges();
            }
        }

        public Author GetAuthorById(int authorId)
        {
            var author = context.Authors.Include(b => b.Books).SingleOrDefault(a => a.Id == authorId);
            return author;
        }

        public List<Author> GetAuthorByName(string searchName)
        {
            var authors = context.Authors.Where(a => a.fullName.StartsWith(searchName)).ToList();
            return authors;
        }

        public List<Author> GetAuthors()
        {
            return context.Authors.Include(b=>b.Books).ToList();
        }
    }
}
