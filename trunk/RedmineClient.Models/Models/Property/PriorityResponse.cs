namespace RedmineClient.Models.Models.Property
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class PriorityResponse
    {
        [JsonProperty("issue_priorities")]
        public List<Priority> Priorities { get; set; }
    }
}
