namespace RedmineClient.Models.Repository
{
    using System.Net;

    /// <summary>
    /// The repository response.
    /// </summary>
    /// <typeparam name="T">
    /// Type of response model
    /// </typeparam>
    public class RepositoryResponse<T>
    {
       /// <summary>
       /// Gets or sets the response object.
       /// </summary>
       public T ResponseObject { get; set; }

       /// <summary>
       /// Gets or sets the status code.
       /// </summary>
       public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the total count.
        /// </summary>
        public int TotalCount { get; set; }
    }
}
