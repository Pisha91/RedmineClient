namespace RedmineClient.Repositories.Abstract.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RedmineClient.Models.Models.Projects;
    using RedmineClient.Models.Repository;

    /// <summary>
    /// The ProjectRepository interface.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Get list of projects projects.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<List<Project>>> GetProjects();
    }
}
