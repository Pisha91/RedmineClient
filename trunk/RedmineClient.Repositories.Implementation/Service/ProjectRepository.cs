namespace RedmineClient.Repositories.Implementation.Service
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Projects;
    using RedmineClient.Models.Proxy;
    using RedmineClient.Models.Repository;
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The project repository.
    /// </summary>
    public class ProjectRepository : BaseServiceRepository, IProjectRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectRepository"/> class.
        /// </summary>
        /// <param name="userCredentialsRepository">
        /// The user credentials repository.
        /// </param>
        /// <param name="webClient">
        /// The web client.
        /// </param>
        public ProjectRepository(IUserCredentialsRepository userCredentialsRepository, IWebClient webClient)
            : base(userCredentialsRepository, webClient)
        {
        }

        /// <summary>
        /// The get projects.
        /// </summary>
        /// <param name="limit">
        /// The limit.
        /// </param>
        /// <param name="offset">
        /// The offset.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<RepositoryResponse<List<Project>>> GetProjects(int limit, int offset)
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

                var response = await this.WebClient.Get(string.Format("projects.json?limit={0}&offset={1}", limit, offset), requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<ProjectsResponse>(result);

                    return new RepositoryResponse<List<Project>>
                    {
                        ResponseObject = issueResponse.Projects,
                        StatusCode = HttpStatusCode.OK,
                        TotalCount = issueResponse.TotalCount
                    };
                }

                return new RepositoryResponse<List<Project>> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync() };
            }

            return new RepositoryResponse<List<Project>> { StatusCode = HttpStatusCode.Unauthorized };
        }
    }
}
