namespace RedmineClient.Repositories.Implementation.Service
{
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;

    /// <summary>
    /// The base service repository.
    /// </summary>
    public class BaseServiceRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseServiceRepository"/> class.
        /// </summary>
        /// <param name="userCredentialsRepository">
        /// The user credentials repository.
        /// </param>
        /// <param name="webClient">
        /// The web client.
        /// </param>
        public BaseServiceRepository(IUserCredentialsRepository userCredentialsRepository, IWebClient webClient)
        {
            this.UserCredentialsRepository = userCredentialsRepository;
            this.WebClient = webClient;
        }

        /// <summary>
        /// Gets or sets the user credentials repository.
        /// </summary>
        protected IUserCredentialsRepository UserCredentialsRepository { get; set; }

        /// <summary>
        /// Gets or sets the web client.
        /// </summary>
        protected IWebClient WebClient { get; set; }
    }
}
