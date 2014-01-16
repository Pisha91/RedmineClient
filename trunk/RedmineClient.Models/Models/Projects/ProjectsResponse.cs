namespace RedmineClient.Models.Models.Projects
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The projects response.
    /// </summary>
    public class ProjectsResponse
    {
        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }

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
