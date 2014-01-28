namespace RedmineClient.Repositories.Implementation.Service
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Issues;
    using RedmineClient.Models.Proxy;
    using RedmineClient.Models.Repository;
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The issue repository.
    /// </summary>
    public class IssueRepository : BaseServiceRepository, IIssueRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueRepository"/> class.
        /// </summary>
        /// <param name="userCredentialsRepository">
        /// The user credentials repository.
        /// </param>
        /// <param name="webClient">
        /// The web client.
        /// </param>
        public IssueRepository(IUserCredentialsRepository userCredentialsRepository, IWebClient webClient)
            : base(userCredentialsRepository, webClient)
        {
        }

        /// <summary>
        /// Return list with issue of log in user.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<RepositoryResponse<List<Issue>>> GetIssues()
        {
            var userCredentials = this.UserCredentialsRepository.Get();
            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest
                                       {
                                           Host = userCredentials.Host,
                                           Username = userCredentials.Username,
                                           Password = userCredentials.Password
                                       };

                var response = await this.WebClient.Get("issues.json", requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<IssuesResponse>(result);

                    return new RepositoryResponse<List<Issue>>
                               {
                                   ResponseObject = issueResponse.Issues,
                                   StatusCode = HttpStatusCode.OK,
                                   TotalCount = issueResponse.TotalCount
                               };
                }

                return new RepositoryResponse<List<Issue>> { StatusCode = response.StatusCode };
            }

            return new RepositoryResponse<List<Issue>> { StatusCode = HttpStatusCode.Unauthorized };
        }

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
        public async Task<RepositoryResponse<List<Issue>>> GetIssuesByUserId(int userId, int limit, int offset)
        {
            var userCredentials = this.UserCredentialsRepository.Get();
            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest
                                       {
                                           Host = userCredentials.Host,
                                           Username = userCredentials.Username,
                                           Password = userCredentials.Password
                                       };

                var response = await this.WebClient.Get(string.Format("issues.json?assigned_to_id={0}&limit={1}&offset={2}", userId, limit, offset), requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<IssuesResponse>(result);

                    return new RepositoryResponse<List<Issue>>
                               {
                                   ResponseObject = issueResponse.Issues,
                                   StatusCode = HttpStatusCode.OK,
                                   TotalCount = issueResponse.TotalCount
                               };
                }

                return new RepositoryResponse<List<Issue>> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync() };
            }

            return new RepositoryResponse<List<Issue>> { StatusCode = HttpStatusCode.Unauthorized };
        }

        /// <summary>
        /// The get issues by project id.
        /// </summary>
        /// <param name="projectId">
        /// The project id.
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
        public async Task<RepositoryResponse<List<Issue>>> GetIssuesByProjectId(int projectId, int limit, int offset)
        {
            var userCredentials = this.UserCredentialsRepository.Get();
            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest
                                       {
                                           Host = userCredentials.Host,
                                           Username = userCredentials.Username,
                                           Password = userCredentials.Password
                                       };

                var response = await this.WebClient.Get(string.Format("issues.json?project_id={0}&limit={1}&offset={2}", projectId, limit, offset), requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<IssuesResponse>(result);

                    return new RepositoryResponse<List<Issue>>
                               {
                                   ResponseObject = issueResponse.Issues,
                                   StatusCode = HttpStatusCode.OK,
                                   TotalCount = issueResponse.TotalCount
                               };
                }

                return new RepositoryResponse<List<Issue>> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync() };
            }

            return new RepositoryResponse<List<Issue>> { StatusCode = HttpStatusCode.Unauthorized };
        }

        /// <summary>
        /// The get issue.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<RepositoryResponse<Issue>> GetIssue(int id)
        {
            var userCredentials = this.UserCredentialsRepository.Get();
            if (userCredentials != null)
            {
                var requestModel = new ProxyRequest
                {
                    Host = userCredentials.Host,
                    Username = userCredentials.Username,
                    Password = userCredentials.Password
                };

                var response = await this.WebClient.Get(string.Format("issues/{0}.json?include=journals,attachments,children", id), requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<IssueResponse>(result);

                    return new RepositoryResponse<Issue>
                    {
                        ResponseObject = issueResponse.Issue,
                        StatusCode = HttpStatusCode.OK
                    };
                }

                return new RepositoryResponse<Issue> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync() };
            }

            return new RepositoryResponse<Issue> { StatusCode = HttpStatusCode.Unauthorized };
        }
    }
}