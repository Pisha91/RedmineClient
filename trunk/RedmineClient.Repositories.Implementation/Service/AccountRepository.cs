namespace RedmineClient.Repositories.Implementation.Service
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RedmineClient.Models.DataBase;
    using RedmineClient.Models.Models.Users;
    using RedmineClient.Models.Proxy;
    using RedmineClient.Models.Repository;
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The account repository.
    /// </summary>
    public class AccountRepository : BaseServiceRepository, IAccountRepository
    {
        /// <summary>
        /// The current user url.
        /// </summary>
        private const string CurrentUserUrl = "users/current.json?include=memberships,groups";

        private const string UserUrl = "users/{0}.json";

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepository"/> class.
        /// </summary>
        /// <param name="userCredentialsRepository">
        /// The user credentials repository.
        /// </param>
        /// <param name="webClient">
        /// The web client.
        /// </param>
        public AccountRepository(IUserCredentialsRepository userCredentialsRepository, IWebClient webClient)
            : base(userCredentialsRepository, webClient)
        {
        }

        /// <summary>
        /// The log in.
        /// </summary>
        /// <param name="host">
        /// The host.
        /// </param>
        /// <param name="username">
        /// The username.
        /// </param>
        /// <param name="password">
        /// The password.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<bool> LogIn(string host, string username, string password)
        {
            var currentCredentials = this.UserCredentialsRepository.Get();
            if (currentCredentials != null)
            {
                this.UserCredentialsRepository.Delete(currentCredentials.Id);
            }

            var requestModel = new ProxyRequest { Host = host, Username = username, Password = password };
            HttpResponseMessage response = await this.WebClient.Get(CurrentUserUrl, requestModel);

            if (response.IsSuccessStatusCode)
            {
                var userCredentials = new UserCredentials { Username = username, Password = password, Host = host };
                UserCredentialsRepository.Add(userCredentials);
                return true;
            }

            return false;
        }

        /// <summary>
        ///  Check that user in data base is authorized.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<bool> IsAuthorized()
        {
            var userCredentials = this.UserCredentialsRepository.Get();
            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest { Host = userCredentials.Host, Username = userCredentials.Username, Password = userCredentials.Password };
                HttpResponseMessage response = await this.WebClient.Get(CurrentUserUrl, requestModel);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The get current user.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<RepositoryResponse<User>> GetCurrentUser()
        {
            var userCredentials = this.UserCredentialsRepository.Get();

            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest { Host = userCredentials.Host, Username = userCredentials.Username, Password = userCredentials.Password };
                HttpResponseMessage response = await this.WebClient.Get(CurrentUserUrl, requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var responseResult = JsonConvert.DeserializeObject<UserResponse>(result);

                    return new RepositoryResponse<User>
                               {
                                   StatusCode = HttpStatusCode.OK,
                                   ResponseObject = responseResult.User
                               };
                }

                return new RepositoryResponse<User> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync() };
            }

            return new RepositoryResponse<User> { StatusCode = HttpStatusCode.Unauthorized };
        }

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<RepositoryResponse<User>> GetUser(int id)
        {
            var userCredentials = this.UserCredentialsRepository.Get();

            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest { Host = userCredentials.Host, Username = userCredentials.Username, Password = userCredentials.Password };
                HttpResponseMessage response = await this.WebClient.Get(string.Format(UserUrl, id), requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var responseResult = JsonConvert.DeserializeObject<UserResponse>(result);

                    return new RepositoryResponse<User>
                               {
                                   StatusCode = HttpStatusCode.OK,
                                   ResponseObject = responseResult.User
                               };
                }

                return new RepositoryResponse<User> { StatusCode = response.StatusCode };
            }

            return new RepositoryResponse<User> { StatusCode = HttpStatusCode.Unauthorized };
        }
    }
}
