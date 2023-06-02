using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFirstProject.Models;
using MyFirstProject.Repositories;
using System.Data;

namespace MyFirstProject.Controllers
{
    public class NewsController : Controller
    {
        //static List<News> staticList = new List<News>();
        NewsRepository newsRepository = new NewsRepository();

        public IActionResult Index()
        {
            IQueryable<News> model = newsRepository.GetAllNews().OrderByDescending(x => x.Date);

            #region withList
            /*
            Listmodel listmodel = new Listmodel();
            listmodel.newsList = newsRepository.GetAllNews(); //ADONET
            listmodel.newsList = staticList; withList
            */
            #endregion

            #region Session
            /*
                var session = HttpContext.Session.GetInt32("UserId");
                if (session == null)
                {
                    return RedirectToAction("Login", "User");
                } 
            */
            #endregion

            return View(model);
        }

        public async Task<IActionResult> DetailsAsync(int id)
        {
            #region withList
            // var news = staticList.FirstOrDefault(x => x.Id == id);
            #endregion

            News? news = await newsRepository.GetNewsAsync(id); 
            if (news is null)
                return View("NotFound");

            return View(news);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddNews()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddNews(News news, [FromForm] IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                if (Image is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(Image.FileName);
                    string filePath = Path.Combine("wwwroot", "assets", "img", fileName);
                    var stream = new FileStream(filePath, FileMode.Create);
                    Image.CopyToAsync(stream);

                    news.Image = $"/assets/img/{fileName}";
                }

                #region withList
                /*
                    news.Id = staticList.Count+1;
                    staticList.Add(news);
                    Listmodel model = new Listmodel();
                    model.newsList = staticList; With List 
                */
                #endregion

                newsRepository.AddNews(news);

                TempData["Message"] = "News has been successfully added!";
                return RedirectToAction("AddNews");
            }
            return View();

        }
    }
}
