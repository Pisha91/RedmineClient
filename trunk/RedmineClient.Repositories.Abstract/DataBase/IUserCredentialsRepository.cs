namespace RedmineClient.Repositories.Abstract.DataBase
{
    using RedmineClient.Models.DataBase;

    /// <summary>
    /// The UserCredentialsRepository interface.
    /// </summary>
    public interface IUserCredentialsRepository
    {
        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="userCredentials">
        /// The user credentials.
        /// </param>
        void Add(UserCredentials userCredentials);

        /// <summary>
        /// The get.
        /// </summary>
        /// <returns>
        /// The <see cref="UserCredentials"/>.
        /// </returns>
        UserCredentials Get();

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        void Delete(int id);
    }
}
