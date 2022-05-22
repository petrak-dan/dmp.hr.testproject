using Microsoft.AspNetCore.Mvc;
using simple.rss.reader.Models;
using System.Diagnostics;

namespace simple.rss.reader.Controllers
{
    public class HomeController : Controller
    {
        private readonly Db _db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, Db db)
        {
            _db = db;
            _logger = logger;
        }

        // GET
        public IActionResult Index()
        {
            DataModel data = new()
            {
                Feeds = _db.Feeds.ToList(),
                Items = _db.Items.ToList(),
                Config = new List<Filter> { 
                    new Filter { From = DateTime.MinValue, To = DateTime.Now } }
            };
            data.Config.Last().From = data.EarliestArticleDate;
            data.DateFrom = data.Config.Last().From;
            data.DateTo = data.Config.Last().To;
            return View(data);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(DataModel data)
        {
            if (ModelState.IsValid)
            {
                var config = _db.Config.ToList();
                _db.RemoveRange(config);
                _db.Config.Update
                    (new Filter
                    {
                        From = data.DateFrom,
                        To = data.DateTo + new TimeSpan(0, 23, 59, 59, 999),
                        Search = string.IsNullOrWhiteSpace(data.SearchString) ? string.Empty : data.SearchString
                    });
                _db.SaveChanges();
            }
            data.Feeds = _db.Feeds.ToList();
            data.Items = _db.Items.ToList();
            data.Config = _db.Config.ToList();
            data.DateFrom = data.Config.Last().From;
            data.DateTo = data.Config.Last().To;
            data.SearchString = data.Config.Last().Search;
            return View(data);
        }

        public IActionResult Reload()
        {
            var config = _db.Config.ToList();
            _db.RemoveRange(config);
            var items = _db.Items.ToList();
            _db.RemoveRange(items);
            foreach (var f in _db.Feeds)
            {
                f.Reload();
                _db.Feeds.Update(f);
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}