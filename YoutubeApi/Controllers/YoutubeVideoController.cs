using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using YoutubeApi.Configuration;
using YoutubeApi.Domain;
using YoutubeApi.Dtos;
using YoutubeApi.Repository;

namespace YoutubeApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class YoutubeVideoController : ControllerBase
    {
        private readonly IYoutubeApiRepository _repo;
        private readonly YoutubeApiSettingsOptions _youtubeApiSettingsOptions;
        public YoutubeVideoController(IYoutubeApiRepository repo, IOptions<YoutubeApiSettingsOptions> youtubeApiSettingsOptions)
        {
            _repo = repo;
            _youtubeApiSettingsOptions = youtubeApiSettingsOptions.Value;
        }

        [HttpGet("getByTitle")]
        public async Task<IActionResult> Get([FromQuery] string title)
        {
            try
            {
                var videos = await _repo.GetYoutubeVideosByTitleAsync(title);
                return Ok(videos);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error trying to get videos by title");
            }
        }

        [HttpGet("getFromYoutube")]
        public async Task<IActionResult> Get([FromQuery] string query, [FromQuery] string pageToken)
        {
            if (string.IsNullOrEmpty(query)) return BadRequest("Please type a query to search");

            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = _youtubeApiSettingsOptions.ApiKey
            });

            SearchResource.ListRequest listRequest = youtubeService.Search.List("snippet");
            listRequest.Q = query;
            listRequest.PageToken = pageToken;
            listRequest.MaxResults = 24;
            listRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            listRequest.Type = "video";


            VideoSearchDto result = new VideoSearchDto();

            SearchListResponse searchResponse = await listRequest.ExecuteAsync();

            result.NextPage = searchResponse.NextPageToken;

            foreach (SearchResult searchResult in searchResponse.Items)
            {
                YoutubeVideo video = new YoutubeVideo
                {
                    Id = searchResult.Id.VideoId,
                    Title = searchResult.Snippet.Title,
                    Thumbnail = searchResult.Snippet.Thumbnails.Medium.Url
                };

                result.Videos.Add(video);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(YoutubeVideo model)
        {
            try
            {
                var video = await _repo.GetYoutubeVideosByIdAsync(model.Id);
                if (video != null) return Ok(video);
                _repo.Add(model);

                if (await _repo.SaveChangesAsync())
                {
                    return Created("", video);
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error trying to insert video");
            }

            return BadRequest();
        }

        [HttpDelete("{videoId}")]
        public async Task<IActionResult> Delete(string videoId)
        {
            try
            {
                var video = await _repo.GetYoutubeVideosByIdAsync(videoId);
                if (video == null) return NotFound();
                _repo.Delete(video);


                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Error trying to remove video");
            }

            return BadRequest();
        }
    }
}
