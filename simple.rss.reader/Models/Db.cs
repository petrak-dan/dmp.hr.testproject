using Microsoft.EntityFrameworkCore;

namespace simple.rss.reader.Models
{
    public class Db: DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {

        }

        public DbSet<Feed> Feeds { get; set; } = null!;

        public DbSet<FeedItem> Items { get; set; } = null!;

        public DbSet<DateFilter> DateConfig { get; set; } = null!;
    }
}
