using Newtonsoft.Json.Linq;


namespace NET_MAUI_BLE.Object.Wifi;

static class HttpResponseMessageExtensions
{
    internal static void WriteRequestToConsole(this HttpResponseMessage response)
    {
        if (response is null)
        {
            return;
        }

        var request = response.RequestMessage;
        Console.Write($"{request?.Method} ");
        Console.Write($"{request?.RequestUri} ");
        Console.WriteLine($"HTTP/{request?.Version}");
    }
}

public class HttpClientManager 
{
    public HttpClientManager(string baseAddress)
    {
        _httpClient = new HttpClient()
        {
            BaseAddress = new Uri(baseAddress),
        };
    }

    private HttpClient _httpClient;

    public async Task<byte[]> GetBinaryAsync(string requestUri)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        return await response.Content.ReadAsByteArrayAsync();
    }

    public async Task<string> GetStringAsync(string requestUri)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<Stream> GetStreamAsync(string requestUri)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        return await response.Content.ReadAsStreamAsync();
    }

    public async Task<JObject> GetAsync(string requestUri)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JObject.Parse(jsonResponse);
    }

    public async Task PostAsync(string requestUri, StringContent content)
    {
        using HttpResponseMessage response = await _httpClient.PostAsync(
            requestUri, content);

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }

    public async Task PutAsync(string requestUri, StringContent content)
    {
        using HttpResponseMessage response = await _httpClient.PutAsync(
            requestUri, content);

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }

    public async Task PatchAsync(string requestUri, StringContent content)
    {
        using HttpResponseMessage response = await _httpClient.PatchAsync(
            requestUri, content);

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }

    public async Task DeleteAsync(string requestUri)
    {
        using HttpResponseMessage response = await _httpClient.DeleteAsync(requestUri);

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");
    }

    public async Task OptionsAsync(string requestUri)
    {
        using HttpRequestMessage request = new(
            HttpMethod.Options, requestUri);

        using HttpResponseMessage response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();

        foreach (var header in response.Content.Headers)
        {
            Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
        }
        Console.WriteLine();
    }
}