using System;
using System.Threading.Tasks;

namespace RestSharp
{
    public static class RestSharpExtensions
    {
        public static Task<T> ExecuteTaskAsync<T>(this RestClient client, RestRequest request) where T : new()
        {
            if (client == null)
            {
                throw new NullReferenceException();
            }

            var tcs = new TaskCompletionSource<T>();

            client.ExecuteAsync<T>(request, (response) =>
            {
                if (response.ErrorException != null)
                {
                    tcs.TrySetException(response.ErrorException);
                }
                else
                {
                    tcs.TrySetResult(response.Data);
                }
            });

            return tcs.Task;
        }

        public static Task<IRestResponse> ExecuteTaskAsync(this RestClient client, RestRequest request)
        {
            if (client == null)
            {
                throw new NullReferenceException();
            }

            var tcs = new TaskCompletionSource<IRestResponse>();

            client.ExecuteAsync(request, (response) =>
            {
                if (response.ErrorException != null)
                {
                    tcs.TrySetException(response.ErrorException);
                }
                else
                {
                    tcs.TrySetResult(response);
                }
            });

            return tcs.Task;
        }
    }
}