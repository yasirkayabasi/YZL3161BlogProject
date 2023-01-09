using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YZL3161BlogProject.Managers;
using YZL3161BlogProject.Models.Data;
using YZL3161BlogProject.Models.Entity;

namespace YZL3161BlogProject.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArticlesController(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public class ArticleController : Controller
        {
            private readonly DatabaseContext _context;
            private readonly IWebHostEnvironment _webHostEnvironment;

            public ArticleController(DatabaseContext context, IWebHostEnvironment webHostEnvironment)
            {
                _context = context;
                _webHostEnvironment = webHostEnvironment;
            }

            public IActionResult Index()
            {
                List<Article> articles = _context.Articles.ToList();
                return View(articles);
            }

            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public IActionResult Create(Article model, IFormFile file)
            {
                if (ModelState.IsValid)
                {
                    model.ArticlePicture = file.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);
                    _context.Articles.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

            [HttpGet]
            public IActionResult Edit(int id)
            {
                Article article = _context.Articles.FirstOrDefault(x => x.Id == id);
                return View(article);
            }

            [HttpPost]
            public IActionResult Edit(Article model, IFormFile file)
            {
                if (ModelState.IsValid)
                {
                    Article article = _context.Articles.FirstOrDefault(x => x.Id == model.Id);
                    article.Title = model.Title;
                    article.Content = model.Content;
                    if (file != null)
                    {
                        article.ArticlePicture = file.GetUniqueNameAndSavePhotoToDisk(_webHostEnvironment);
                    }
                    _context.Articles.Update(article);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }

            [HttpPost]
            public IActionResult Delete(int id)
            {
                Article article = _context.Articles.FirstOrDefault(x => x.Id == id);
                _context.Articles.Remove(article);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}
