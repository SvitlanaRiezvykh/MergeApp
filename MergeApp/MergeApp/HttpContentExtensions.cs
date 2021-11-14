using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace MergeApp
{
    public static class HttpContentExtensions
    {
        private static readonly JsonSerializer Serializer = JsonSerializer.CreateDefault();

        public static async Task<T> ReadAsType<T>(this HttpContent content)
        {
            using Stream stream = await content.ReadAsStreamAsync();
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                T result = Serializer.Deserialize<T>(jsonReader);

                return result;
            }
        }
    }
}
