using MergeApp.Contracts;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MergeApp
{
    public class TuneInService
    {
        private const string ServiceUrl = "https://api.fns.tunein.com/v1";
        private const string Endpoint = "json";
        private const string MediaType = "application/json";

        private readonly HttpClient httpClient;

        public TuneInService() : this(new HttpClient()) { }

        public TuneInService(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            var acceptHeader = new MediaTypeWithQualityHeaderValue(MediaType);
            this.httpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
        }

        public async Task<List<Ranked>> GetJsonResponse()
        {
            var uri = new Uri($"{ServiceUrl}/{Endpoint}");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri
            };

            var responseMessage = await this.httpClient.SendAsync(request);
            responseMessage.EnsureSuccessStatusCode();

            var jsonResponse = await responseMessage.Content.ReadAsType<GetJsonResponse>();
            return jsonResponse?.Ranked;
        }
    }
}
