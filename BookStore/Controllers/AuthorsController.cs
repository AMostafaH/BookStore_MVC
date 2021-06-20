using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Models.ManageAuthor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IManageAuthor manageAuthor;

        [BindProperty(SupportsGet =true)]
        public string SearchName { get; set; }

        public AuthorsController(IManageAuthor _manageAuthor)
        {
            manageAuthor = _manageAuthor;
        }
        // GET: AuthorsController
        public ActionResult Index()
        {
            //ViewData["SearchName"] = SearchName;
            if (SearchName == null)
            {
                var authors = manageAuthor.GetAuthors();
                return View(authors);
            }
            else
            {
                var authors = manageAuthor.GetAuthorByName(SearchName).ToList();
                return View(authors);
            }   
        }

        // GET: AuthorsController/Details/5
        public ActionResult Details(int id)
        {
            var author = manageAuthor.GetAuthorById(id);
            return View(author);
        }

        // GET: AuthorsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuthorsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author author)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    manageAuthor.AddAuthor(author);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            ModelState.AddModelError("", "You have to fill all the required fields!");
            return View();

        }

        // GET: AuthorsController/Edit/5
        public ActionResult Edit(int id)
        {
            var author = manageAuthor.GetAuthorById(id);
            return View(author);
        }

        // POST: AuthorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Author author)
        {
            try
            {
                manageAuthor.EditAuthor(author);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuthorsController/Delete/5
        public ActionResult Delete(int id)
        {
            var author = manageAuthor.GetAuthorById(id);
            return View(author);
        }

        // POST: AuthorsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Author author)
        {
            try
            {
                manageAuthor.DeleteAuthor(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
