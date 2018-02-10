using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorMgr.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthorMgr.Controllers
{
    [Authorize]  // controller available only if someone logged in
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepo;  // generate a private repository, our local copy
        private readonly ILogger<AuthorController> _log;

        public AuthorController(IAuthorRepository authorRepo, ILogger<AuthorController> log)  // create a controller, authorRepo w/o underscore is passed in from the outside
        {
            _authorRepo = authorRepo;
            _log = log;
        }


        // GET: Author
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(_authorRepo.ListAll());
        }

        // GET: Author/Details/5
        public ActionResult Details(int id)
        {
            _log.LogInformation("Received Request for Author Id: {id}", id);
            return View(_authorRepo.GetById(id));
        }

        // GET: Author/Create
        public ActionResult Create(Author newAuthor)
        {
            return View();
        }

        // POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Author newAuthor, IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return View(newAuthor);
            }
            try
            {
                _authorRepo.AddAuthor(newAuthor);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(newAuthor);
            }
        }

        // GET: Author/Edit/5
        public ActionResult Edit(int id)
        {

            return View(_authorRepo.GetById(id));
        }

        // POST: Author/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Author editAuthor, int id, IFormCollection collection)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(editAuthor);
                }
                try
                {
                    _authorRepo.EditAuthor(editAuthor);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View(editAuthor);
                }

                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Author/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Author/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}