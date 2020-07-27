using Microsoft.EntityFrameworkCore;
using YoutubeApi.Domain;

namespace YoutubeApi.Repository
{
    public class YoutubeApiContext : DbContext
    {
        public YoutubeApiContext(DbContextOptions<YoutubeApiContext> options) : base(options) { }

        public DbSet<YoutubeVideo> YoutubeVideos { get; set; }
    }
}
