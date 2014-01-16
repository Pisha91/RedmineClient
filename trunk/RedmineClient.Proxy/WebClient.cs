namespace RedmineClient.Proxy
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    using Newtonsoft.Json;

    using RedmineClient.Models.Proxy;

    /// <summary>
    /// The WebClient interface.
    /// </summary>
    public interface IWebClient
    {
        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<HttpResponseMessage> Get(string url, ProxyRequest requestModel);

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <typeparam name="T">
        /// Model that we put to post request.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<HttpResponseMessage> Post<T>(string url, T model, ProxyRequest requestModel);

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <typeparam name="T">
        /// Model that we put to post request.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<HttpResponseMessage> Put<T>(string url, T model, ProxyRequest requestModel);

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<HttpResponseMessage> Delete(string url, ProxyRequest requestModel);
    }

    /// <summary>
    /// The web client.
    /// </summary>
    public class WebClient : IWebClient
    {
        /// <summary>
        /// The client.
        /// </summary>
        private HttpClient client;

        /// <summary>
        /// The host name.
        /// </summary>
        private string hostName;

        /// <summary>
        /// The handler.
        /// </summary>
        private HttpClientHandler handler;

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> Get(string url, ProxyRequest requestModel)
        {
            this.InitHttpClient(requestModel);
            HttpResponseMessage response = await this.client.GetAsync(url);

            return response;
        }

        /// <summary>
        /// The post.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <typeparam name="T">
        /// Model that we put to post request.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> Post<T>(string url, T model, ProxyRequest requestModel)
        {
            this.InitHttpClient(requestModel);
            string stringContent = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(stringContent);
            HttpResponseMessage response = await this.client.PostAsync(url, content);

            return response;
        }

        /// <summary>
        /// The put.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <typeparam name="T">
        /// Model that we put to post request.
        /// </typeparam>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> Put<T>(string url, T model, ProxyRequest requestModel)
        {
            this.InitHttpClient(requestModel);
            string stringContent = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(stringContent);
            HttpResponseMessage response = await this.client.PutAsync(url, content);

            return response;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<HttpResponseMessage> Delete(string url, ProxyRequest requestModel)
        {
            this.InitHttpClient(requestModel);
            HttpResponseMessage response = await this.client.DeleteAsync(url);

            return response;
        }

        /// <summary>
        /// The initialize http client.
        /// </summary>
        /// <param name="requestModel">
        /// The request model.
        /// </param>
        private void InitHttpClient(ProxyRequest requestModel)
        {
            this.hostName = requestModel.Host;
            this.handler = new HttpClientHandler
                               {
                                   CookieContainer = new CookieContainer(),
                                   Credentials = new NetworkCredential(requestModel.Username, requestModel.Password)
                               };
            this.client = new HttpClient(this.handler) { BaseAddress = new Uri(this.hostName) };
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
