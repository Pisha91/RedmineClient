namespace RedmineClient.Models.Models.Projects
{
    using System;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Common;

    /// <summary>
    /// The project.
    /// </summary>
    public class Project
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

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [JsonProperty("created_on")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        [JsonProperty("updated_on")]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        [JsonProperty("parent")]
        public Label Parent { get; set; }

        /// <summary>
        /// Gets the created on string.
        /// </summary>
        [JsonIgnore]
        public string CreatedOnString
        {
            get
            {
                return this.CreatedOn.ToString("d");
            }
        }

        /// <summary>
        /// Gets the show like parent.
        /// </summary>
        [JsonIgnore]
        public string ShowLikeParent
        {
            get
            {
                return this.Parent != null ? "Collapsed" : "Visible";
            }
        }

        /// <summary>
        /// Gets the show like sub.
        /// </summary>
        [JsonIgnore]
        public string ShowLikeSub
        {
            get
            {
                return this.Parent == null ? "Collapsed" : "Visible";
            }
        }
    }
}
