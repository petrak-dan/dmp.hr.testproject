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
            DataModel data = new();
            data.Feeds = _db.Feeds.ToList();
            data.Items = _db.Items.ToList();
            data.Config = _db.Config.ToList();
            return View(data);
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
                f.Reload();
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
                var items = _db.Items.Where(item => item.FeedId == f.Id);
                _db.RemoveRange(items);
                f.Reload();
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

            var items = _db.Items.Where(item => item.FeedId == f.Id);
            _db.RemoveRange(items);
            _db.SaveChanges();
            _db.Feeds.Remove(f);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Info(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var f = _db.Feeds.FirstOrDefault(x => x.Id == id);
            if (f == null) { return NotFound(); }
            f.Items = _db.Items.Where(x => x.FeedId == f.Id).ToList();
            return View(f);
        }

        // GET
        public IActionResult Reload(int? id)
        {
            if (id == null || id == 0) { return NotFound(); }
            var f = _db.Feeds.FirstOrDefault(x => x.Id == id);
            if (f == null) { return NotFound(); }
            var items = _db.Items.Where(item => item.FeedId == f.Id);
            _db.RemoveRange(items);
            f.Reload();
            _db.SaveChanges();
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
