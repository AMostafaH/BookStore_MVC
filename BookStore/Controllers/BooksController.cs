using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.ManageAuthor;
using BookStore.Models.ManageBook;
using BookStore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IManageBook manageBook;
        private readonly IManageAuthor manageAuthor;
        private readonly IWebHostEnvironment hostingEnvironment; // Used to deal with Files

        [BindProperty(SupportsGet =true)]
        public string SearchName { get; set; }

        public BooksController(IManageBook _manageBook, IManageAuthor _manageAuthor, IWebHostEnvironment _hostingEnvironment)
        {
            manageBook = _manageBook;
            manageAuthor = _manageAuthor;
            hostingEnvironment = _hostingEnvironment;
        }
        // GET: BooksController
        public ActionResult Index()
        {
            if (SearchName == null)
            {
                var books = manageBook.GetBooks();
                return View(books);
            }
            else
            {
                var authors = manageBook.GetBookByName(SearchName).ToList();
                return View(authors);
            }
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
            var book = manageBook.GetBookById(id);
            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View(GetAllAuthors());
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string fileName = null;
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(hostingEnvironment.WebRootPath, "Uploads");
                        fileName = model.File.FileName;
                        string fullPath = Path.Combine(uploads, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            model.File.CopyTo(stream);
                        }
                    }
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = manageAuthor.GetAuthorById(model.AuthorId),
                        ImageURL = fileName
                    };
                    manageBook.AddBook(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View(GetAllAuthors());

        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = manageBook.GetBookById(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id;
            var bookauthorViewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Description = book.Description,
                Title = book.Title,
                AuthorId = authorId,
                Authors = manageAuthor.GetAuthors().ToList(),
                ImageURL = book.ImageURL
            };
            return View(bookauthorViewModel);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookAuthorViewModel viewModel)
        {
            try
            {
                string fileName = null;
                if (viewModel.File != null)
                {
                    string uploads = Path.Combine(hostingEnvironment.WebRootPath, "uploads");
                    fileName = viewModel.File.FileName;
                    string fullPath = Path.Combine(uploads, fileName);

                    //Delete the Old path
                    //string oldFileName = manageBook.GetBookById(viewModel.BookId).ImageURL;
                    string oldFileName = "default.jpg"; // set default name to pass the error

                    if(viewModel.ImageURL != null)
                    {
                        oldFileName = viewModel.ImageURL;
                    }

                    string fullOldPath = Path.Combine(uploads, oldFileName);
                    
                    if (fullPath != fullOldPath)
                    {
                        if(System.IO.File.Exists(fullOldPath))
                            System.IO.File.Delete(fullOldPath);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            viewModel.File.CopyTo(stream);
                        }                        
                    }
                }
                var book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = manageAuthor.GetAuthorById(viewModel.AuthorId),
                    ImageURL = fileName
                };
                manageBook.EditBook(viewModel.BookId, book);
                //manageBook.EditBook(book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = manageBook.GetBookById(id);
            return View(book);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            try
            {
                manageBook.DeleteBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = manageAuthor.GetAuthors().ToList()
            };
            return vmodel;
        }
    }
}
