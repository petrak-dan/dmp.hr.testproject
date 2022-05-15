using Microsoft.EntityFrameworkCore;

namespace simple.rss.reader.Models
{
    public class Db: DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {

        }

        public DbSet<Feed> Feeds { get; set; } = null!;
    }
}
