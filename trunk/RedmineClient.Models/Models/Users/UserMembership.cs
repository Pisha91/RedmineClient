namespace RedmineClient.Models.Models.Users
{
    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Common;

    /// <summary>
    /// The user membership.
    /// </summary>
    public class UserMembership
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        [JsonProperty("project")]
        public Label Project { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        [JsonProperty("roles")]
        public Label[] Roles { get; set; }
    }
}
