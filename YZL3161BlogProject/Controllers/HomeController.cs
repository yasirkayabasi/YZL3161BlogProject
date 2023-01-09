using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YZL3161BlogProject.Filters;
using YZL3161BlogProject.Models;
using YZL3161BlogProject.Models.Data;
using YZL3161BlogProject.Models.Entity;

namespace YZL3161BlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;
        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            string userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            List<Article> articles = _context.Articles.ToList();
            return View(articles);
        }

        public IActionResult Details(int id)
        {
            string userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Auth");
            }
            Article article = _context.Articles.FirstOrDefault(x => x.Id == id);
            return View(article);
        }
    }
}
