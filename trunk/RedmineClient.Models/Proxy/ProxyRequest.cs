namespace RedmineClient.Models.Proxy
{
    /// <summary>
    /// The proxy request model.
    /// </summary>
    public class ProxyRequest
    {
        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
