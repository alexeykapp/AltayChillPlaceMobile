using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AltayChillPlace.ApiResponses.Admin
{
    public class ApplicationStatus
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("updateDate")]
        public DateTime UpdateDate { get; set; }

        [JsonProperty("updateTime")]
        public TimeSpan UpdateTime { get; set; }

        [JsonProperty("applicationStatus")]
        public int ApplicationStatuses { get; set; }

        [JsonProperty("statusName")]
        public string StatusName { get; set; }
    }
}
