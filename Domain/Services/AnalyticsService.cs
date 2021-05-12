using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class AnalyticsService : IThirdPartyService
    {
        private readonly HttpClient httpClient;

        public AnalyticsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> SendRequestToThirdParty(Guid orderId)
        {
            var response = await httpClient.PostAsJsonAsync("api/post", orderId);
            if (response.IsSuccessStatusCode == false)
            {
                throw new Exception("Response was not successful.");
            }

            return await response.Content.ReadAsAsync<string>();
        }
    }
}
