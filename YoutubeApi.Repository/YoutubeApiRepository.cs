using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using YoutubeApi.Domain;

namespace YoutubeApi.Repository
{
    public class YoutubeApiRepository : IYoutubeApiRepository
    {
        private readonly YoutubeApiContext _context;

        public YoutubeApiRepository(YoutubeApiContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<YoutubeVideo[]> GetYoutubeVideosByTitleAsync(string title)
        {
            IQueryable<YoutubeVideo> query = _context.YoutubeVideos
                .OrderBy(v => v.Title)
                .Where(v => v.Title.ToLower().Contains(title.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<YoutubeVideo> GetYoutubeVideosByIdAsync(string videoId)
        {
            IQueryable<YoutubeVideo> query = _context.YoutubeVideos
                        .Where(v => v.Id == videoId);

            return await query.FirstOrDefaultAsync();
        }
    }
}
