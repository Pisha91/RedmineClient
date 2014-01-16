namespace RedmineClient.Models.Models.Status
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class IssueStatusesResponse
    {
        [JsonProperty("issue_statuses")]
        public List<Status> Statuses;
    }
}
