using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses
{
    public class BlogPostsResponce
    {
        [JsonProperty("id_blog")]
        public int Id { get; set; }

        [JsonProperty("publication_title")]
        public string Title { get; set; }

        [JsonProperty("publication_date")]
        public string PublicationDate { get; set; }

        [JsonProperty("publication_text")]
        public string Content { get; set; }

        [JsonProperty("image_blog")]
        public PhotoResponse Image { get; set; }
    }
}
