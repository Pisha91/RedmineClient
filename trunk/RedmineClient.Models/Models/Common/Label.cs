namespace RedmineClient.Models.Models.Common
{
    using Newtonsoft.Json;

    /// <summary>
    /// The label.
    /// </summary>
    public class Label
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
