using Newtonsoft.Json.Linq;
using NET_MAUI_BLE.API;


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

    public async Task<JObject> GetAsync(string requestUri)
    {
        using HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
        
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return JObject.Parse(jsonResponse);    
    }

    // TODO - Move this file to WifiController
    public async Task<string> GetAudio()
    {
        using HttpResponseMessage response = await _httpClient.GetAsync("/audio");
        response.EnsureSuccessStatusCode()
            .WriteRequestToConsole();
        var unique_id = FileAPI.GetUniqueID();
        var fileName = $"{unique_id}.wav";
        try
        {
            var binaryData = await response.Content.ReadAsByteArrayAsync();
            FileAPI.WriteCacheData(fileName, binaryData);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception occurred");
        }
        return FileAPI.GetCachePath(fileName);
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