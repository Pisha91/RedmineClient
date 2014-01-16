namespace RedmineClient.Repositories.Implementation.Service
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Status;
    using RedmineClient.Models.Proxy;
    using RedmineClient.Models.Repository;
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    /// <summary>
    /// The IssueStatusRepository interface.
    /// </summary>
    public class IssueStatusRepository : BaseServiceRepository, IIssueStatusRepository
    {
        public IssueStatusRepository(IUserCredentialsRepository userCredentialsRepository, IWebClient webClient)
            : base(userCredentialsRepository, webClient)
        {
        }

        public async Task<RepositoryResponse<List<Status>>> GetStatuses()
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

                var response = await this.WebClient.Get("issue_statuses.json", requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<IssueStatusesResponse>(result);

                    return new RepositoryResponse<List<Status>>
                               {
                                   ResponseObject = issueResponse.Statuses,
                                   StatusCode = HttpStatusCode.OK
                               };
                }

                return new RepositoryResponse<List<Status>> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync()};
            }

            return new RepositoryResponse<List<Status>> { StatusCode = HttpStatusCode.Unauthorized };
        }
    }
}
