namespace RedmineClient.Repositories.Implementation.DataBase
{
    using System.Linq;

    using RedmineClient.Data;
    using RedmineClient.Models.DataBase;
    using RedmineClient.Repositories.Abstract.DataBase;

    /// <summary>
    /// The user credentials repository.
    /// </summary>
    public class UserCredentialsRepository : IUserCredentialsRepository
    {
        /// <summary>
        /// The data base manager.
        /// </summary>
        private readonly IDataBaseManager dataBaseManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserCredentialsRepository"/> class.
        /// </summary>
        /// <param name="dataBaseManager">
        /// The data base manager.
        /// </param>
        public UserCredentialsRepository(IDataBaseManager dataBaseManager)
        {
            this.dataBaseManager = dataBaseManager;
            dataBaseManager.CreateTable<UserCredentials>();
        }

        /// <summary>
        /// The add. Insert record to table UserCredentials
        /// </summary>
        /// <param name="userCredentials">
        /// The user credentials.
        /// </param>
        public void Add(UserCredentials userCredentials)
        {
           this.dataBaseManager.Insert(userCredentials);
        }

        /// <summary>
        /// The get. Return credentials of current user from table UserCredentials
        /// </summary>
        /// <returns>
        /// The <see cref="UserCredentials"/>.
        /// </returns>
        public UserCredentials Get()
        {
            return this.dataBaseManager.GetList<UserCredentials>().FirstOrDefault();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public void Delete(int id)
        {
            this.dataBaseManager.Delete<UserCredentials, int>(id);
        }
    }
}
