namespace RedmineClient.Repositories.Abstract.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RedmineClient.Models.Models.Issues;
    using RedmineClient.Models.Repository;

    /// <summary>
    /// The IssueRepository interface.
    /// </summary>
    public interface IIssueRepository
    {
        /// <summary>
        /// The get issues.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<List<Issue>>> GetIssues();

        /// <summary>
        /// The get issues.
        /// </summary>
        /// <param name="userId">
        /// The user id.
        /// </param>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<List<Issue>>> GetIssues(int userId, int limit, int offset);

        /// <summary>
        /// The get issue.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<RepositoryResponse<Issue>> GetIssue(int id);
    }
}
