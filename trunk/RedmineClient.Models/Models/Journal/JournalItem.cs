namespace RedmineClient.Models.Models.Journal
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Common;

    /// <summary>
    /// The journal item.
    /// </summary>
    public class JournalItem
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [JsonProperty("user")]
        public Label User { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [JsonProperty("created_on")]
        public DateTimeOffset CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        [JsonProperty("details")]
        public List<JournalDetails> Details { get; set; }

        /// <summary>
        /// Gets the updated on string.
        /// </summary>
        [JsonIgnore]
        public string UpdatedOnString
        {
            get
            {
                return string.Format("({0})", this.CreatedOn.ToString("dd-MM-yyyy HH:mm"));
            }
        }
    }
}
