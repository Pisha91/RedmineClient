namespace RedmineClient.Data
{
    using System.Collections.Generic;

    /// <summary>
    /// The DataBaseManager interface.
    /// </summary>
    public interface IDataBaseManager
    {
        /// <summary>
        /// The create table.
        /// </summary>
        /// <typeparam name="T">
        /// Type of table that we want to create
        /// </typeparam>
        void CreateTable<T>();

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <typeparam name="T">
        /// Type of entity that we want insert to data base
        /// </typeparam>
        void Insert<T>(T item);

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
        void Delete<TItem, TId>(TId id);

        /// <summary>
        /// The get list.
        /// </summary>
        /// <typeparam name="TItem">
        /// Type of entity that we want get from data base
        /// </typeparam>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        List<TItem> GetList<TItem>() where TItem : new();

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
        TItem Get<TItem, TId>(TId id) where TItem : new();
    }
}
