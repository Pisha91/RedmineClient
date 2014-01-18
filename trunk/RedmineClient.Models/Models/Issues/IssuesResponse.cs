namespace RedmineClient.Models.Models.Issues
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The issue response.
    /// </summary>
    public class IssuesResponse
    {
        /// <summary>
        /// Gets or sets the issues.
        /// </summary>
        [JsonProperty("issues")]
        public List<Issue> Issues { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        [JsonProperty("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        [JsonProperty("offset")]
        public int Offset { get; set; }
    }
}
