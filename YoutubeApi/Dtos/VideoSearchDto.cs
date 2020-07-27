using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YoutubeApi.Domain;

namespace YoutubeApi.Dtos
{
    public class VideoSearchDto
    {
        public string NextPage { get; set; }
        public List<YoutubeVideo> Videos { get; set; }

        public VideoSearchDto()
        {
            Videos = new List<YoutubeVideo>();
        }
    }
}
