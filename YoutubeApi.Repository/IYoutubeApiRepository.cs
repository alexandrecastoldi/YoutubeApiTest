using System.Threading.Tasks;
using YoutubeApi.Domain;

namespace YoutubeApi.Repository
{
    public interface IYoutubeApiRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //YOUTUBE VIDEOS
        Task<YoutubeVideo[]> GetYoutubeVideosByTitleAsync(string title);
        Task<YoutubeVideo> GetYoutubeVideosByIdAsync(string videoId);
    }
}
