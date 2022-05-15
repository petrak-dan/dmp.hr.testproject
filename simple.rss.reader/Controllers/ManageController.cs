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

        // GET
        public IActionResult Add()
        {
            return View();
        }

        // POST
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

        // GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var f = _db.Feeds.FirstOrDefault(x => x.Id == id);
            if (f == null) { return NotFound(); }
            return View(f);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Feed f)
        {
            if (ModelState.IsValid)
            {
                _db.Feeds.Update(f);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }

        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var f = _db.Feeds.FirstOrDefault(x => x.Id == id);
            if (f == null) { return NotFound(); }
            return View(f);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFeed(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var f = _db.Feeds.FirstOrDefault(x => x.Id == id);
            if (f == null) { return NotFound(); }
            _db.Feeds.Remove(f);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
