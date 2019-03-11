using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using TestCinephiles.Exceptions;

namespace TestCinephiles.Services.RequestProvider
{
    public class RequestProvider : IRequestProvider
    {
        private const string DefaultMediaTypeHeaderValue = "application/json";
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            var httpClient = CreateHttpClient();
            var response = await httpClient.GetAsync(uri).ConfigureAwait(false);

            await HandleResponse(response);
            using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                return JsonConvert.DeserializeObject<TResult>(
                    await new StreamReader(responseStream)
                    .ReadToEndAsync().ConfigureAwait(false), _serializerSettings);
            }       
        }

        public string ConvertQueryString(object queryStringObject)
        {
            var properties = from p in queryStringObject.GetType().GetProperties()
                             where p.GetValue(queryStringObject, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(queryStringObject, null).ToString());

            return $"?{String.Join("&", properties.ToArray())}";
        }

        private HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(DefaultMediaTypeHeaderValue));

            if (!string.IsNullOrWhiteSpace(token))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return httpClient;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                return;

            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == HttpStatusCode.Forbidden ||
                response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new ServiceAuthenticationException(content);
            }

            throw new HttpRequestExceptionEx(response.StatusCode, content);
        }
    }
}
