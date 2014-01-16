namespace RedmineClient.Repositories.Abstract.Service
{
    using System.Threading.Tasks;

    using RedmineClient.Models.Models.Users;
    using RedmineClient.Models.Repository;

    /// <summary>
    /// The i account repository.
    /// </summary>
    public interface IAccountRepository
    {
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
        /// The <see cref="bool"/>.
        /// </returns>
        Task<bool> LogIn(string host, string username, string password);

        /// <summary>
        /// The is authorized.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        Task<bool> IsAuthorized();

        /// <summary>
        /// The get current user.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<User>> GetCurrentUser();

        /// <summary>
        /// The get user.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<User>> GetUser(int id);
    }
}
