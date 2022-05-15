using Microsoft.AspNetCore.Mvc;
using simple.rss.reader.Models;

namespace simple.rss.reader.Controllers
{
    public class ManageController : Controller
    {
        private readonly Db _db;

        public ManageController(Db db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Feed> feeds = _db.Feeds;
            return View(feeds);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Feed f)
        {
            if (ModelState.IsValid)
            {
                _db.Feeds.Add(f);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }
    }
}
