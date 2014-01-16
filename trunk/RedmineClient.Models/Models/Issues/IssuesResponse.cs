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
    }
}
