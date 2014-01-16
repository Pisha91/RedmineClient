namespace RedmineClient.Models.Models.Property
{
    using Newtonsoft.Json;
    public class Priority
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }
    }
}
