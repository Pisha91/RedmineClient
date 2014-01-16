namespace RedmineClient.Repositories.Abstract.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RedmineClient.Models.Models.Status;
    using RedmineClient.Models.Repository;

    /// <summary>
    /// The StatusRepository interface.
    /// </summary>
    public interface IIssueStatusRepository
    {
        Task<RepositoryResponse<List<Status>>> GetStatuses();
    }
}
