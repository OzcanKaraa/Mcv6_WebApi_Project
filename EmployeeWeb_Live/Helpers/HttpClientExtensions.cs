using System.Runtime.CompilerServices;
using System.Text.Json;

namespace EmployeeWeb_Live.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAsync<T> (this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode == false)
                throw new ApplicationException($"API çağrılırken problem : { response.ReasonPhrase}");

            // api den gelen veri okunuyor.
            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Json verisi ayarlanıyor.
            var result = JsonSerializer.Deserialize<T>(dataAsString,new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive= true
            });

            return result;


        }

    }
}
