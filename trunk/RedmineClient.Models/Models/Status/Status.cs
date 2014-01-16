namespace RedmineClient.Models.Models.Status
{
    using Newtonsoft.Json;

    public class Status
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        [JsonProperty("is_closed")]
        public bool IsClosed { get; set; }
    }
}
