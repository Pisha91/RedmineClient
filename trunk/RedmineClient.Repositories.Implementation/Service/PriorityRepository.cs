namespace RedmineClient.Repositories.Implementation.Service
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RedmineClient.Models.Models.Property;
    using RedmineClient.Models.Proxy;
    using RedmineClient.Models.Repository;
    using RedmineClient.Proxy;
    using RedmineClient.Repositories.Abstract.DataBase;
    using RedmineClient.Repositories.Abstract.Service;

    public class PriorityRepository : BaseServiceRepository, IPriorityRepository
    {
        public PriorityRepository(IUserCredentialsRepository userCredentialsRepository, IWebClient webClient)
            : base(userCredentialsRepository, webClient)
        {
        }

        public async Task<RepositoryResponse<List<Priority>>> GetPriorities()
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

                var response = await this.WebClient.Get("enumerations/issue_priorities.json", requestModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var issueResponse = JsonConvert.DeserializeObject<PriorityResponse>(result);

                    return new RepositoryResponse<List<Priority>>
                               {
                                   ResponseObject = issueResponse.Priorities,
                                   StatusCode = HttpStatusCode.OK
                               };
                }

                return new RepositoryResponse<List<Priority>> { StatusCode = response.StatusCode, Message = await response.Content.ReadAsStringAsync()};
            }

            return new RepositoryResponse<List<Priority>> { StatusCode = HttpStatusCode.Unauthorized };
        }
    }
}
