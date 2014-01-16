namespace RedmineClient.Models.DataBase
{
    using SQLite;

    /// <summary>
    /// The user credentials.
    /// </summary>
    public class UserCredentials
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        public string Host { get; set; }
    }
}
