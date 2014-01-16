namespace RedmineClient.Repositories.Abstract.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RedmineClient.Models.Models.Property;
    using RedmineClient.Models.Repository;

    /// <summary>
    /// The PriorityRepository interface.
    /// </summary>
    public interface IPriorityRepository
    {
        /// <summary>
        /// The get priorities.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<List<Priority>>> GetPriorities();
    }
}
