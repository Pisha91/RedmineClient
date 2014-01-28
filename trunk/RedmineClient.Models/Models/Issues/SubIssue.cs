namespace RedmineClient.Models.Models.Issues
{
    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Common;

    /// <summary>
    /// The sub issue.
    /// </summary>
    public class SubIssue
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the tracker.
        /// </summary>
        [JsonProperty("tracker")]
        public Label Tracker { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        [JsonIgnore]
        public string Title
        {
            get
            {
                return string.Format("{0} #{1}", this.Tracker.Name, this.Id);
            }
        } 
    }
}
