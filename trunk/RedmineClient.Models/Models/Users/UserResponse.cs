namespace RedmineClient.Models.Models.Users
{
    using Newtonsoft.Json;

    /// <summary>
    /// The user response.
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
