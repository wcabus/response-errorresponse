using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp2.Http
{
    public abstract class HttpService
    {
        private static readonly HttpClient Client = new HttpClient
        {
            DefaultRequestHeaders =
            {
                Accept =
                {
                    new MediaTypeWithQualityHeaderValue("application/json")
                }
            }
        };

        protected async Task<IResponse<T>> Get<T>(string uri)
        {
            HttpResponseMessage response = null;
            string content = null;

            try
            {
                response = await Client.GetAsync(uri)
                    .ConfigureAwait(false);

                content = await response.Content.ReadAsStringAsync()
                    .ConfigureAwait(false);

                return response.IsSuccessStatusCode
                    ? FormResponseForJson<T>(response.StatusCode, content)
                    : new ErrorResponse<T>(response.StatusCode, content);
            }
            catch (Exception e)
            {
                return new ErrorResponse<T>(response?.StatusCode ?? HttpStatusCode.InternalServerError, e.Message)
                {
                    OriginalData = content,
                    Exception = e
                };
            }
        }

        private IResponse<T> FormResponseForJson<T>(HttpStatusCode statusCode, string json)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<T>(json);
                return new Response<T>(statusCode, data);
            }
            catch (Exception e)
            {
                return new ErrorResponse<T>(statusCode, "Couldn't deserialize the received data to the requested type.")
                {
                    OriginalData = json,
                    Exception = e
                };
            }
        }
    }
}