namespace RedmineClient.Models.Models.Issues
{
    using Newtonsoft.Json;

    /// <summary>
    /// The issue response.
    /// </summary>
    public class IssueResponse
    {
        /// <summary>
        /// Gets or sets the issue.
        /// </summary>
        [JsonProperty("issue")]
        public Issue Issue { get; set; }
    }
}
