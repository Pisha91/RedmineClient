namespace RedmineClient.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using SQLite;

    /// <summary>
    /// The data base manager.
    /// </summary>
    public class DataBaseManager : IDataBaseManager
    {
        /// <summary>
        /// The sql lite connection.
        /// </summary>
        private readonly SQLiteConnection sqLiteConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataBaseManager"/> class.
        /// </summary>
        /// <param name="connectionString">
        /// The connection String.
        /// </param>
        public DataBaseManager(string connectionString)
        {
            this.sqLiteConnection = new SQLiteConnection(connectionString);
        }

        /// <summary>
        /// The create table.
        /// </summary>
        /// <typeparam name="T">
        /// Type of table that we want to create
        /// </typeparam>
        public void CreateTable<T>()
        {
            this.sqLiteConnection.CreateTable<T>();
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <typeparam name="T">
        /// Type of entity that we want insert to data base
        /// </typeparam>
        public void Insert<T>(T item)
        {
            this.sqLiteConnection.Insert(item);
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <typeparam name="TItem">
        /// Type of entity that we want delete from data base
        /// </typeparam>
        /// <typeparam name="TId">
        /// Type of id parameter
        /// </typeparam>
        public void Delete<TItem, TId>(TId id)
        {
            this.sqLiteConnection.Delete<TItem>(id);
        }

        /// <summary>
        /// The get list.
        /// </summary>
        /// <typeparam name="TItem">
        /// Type of entity that we want get from data base
        /// </typeparam>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public List<TItem> GetList<TItem>() where TItem : new()
        {
            List<TItem> items = this.sqLiteConnection.Table<TItem>().ToList<TItem>();
            return items;
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <typeparam name="TItem">
        /// Type of entity that we want get from data base
        /// </typeparam>
        /// <typeparam name="TId">
        /// Type of id parameter
        /// </typeparam>
        /// <returns>
        /// The <see cref="TItem"/>.
        /// </returns>
        public TItem Get<TItem, TId>(TId id) where TItem : new()
        {
            return this.sqLiteConnection.Get<TItem>(id);
        }
    }
}
