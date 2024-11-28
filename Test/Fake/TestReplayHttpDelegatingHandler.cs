using System.Security.Cryptography;
using System.Text;
using LLMIntegration.Util;

namespace Test.Fake;

public class TestReplayHttpDelegatingHandler : DelegatingHandler
{
    private const string CacheDirectory = "TestResponseCache";
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var isStream = request.Headers.TryGetValues(LlmConstants.LlmIsStreamHeaderName, out _);
        var requestBody = request.Content != null
            ? await request.Content.ReadAsStringAsync(cancellationToken)
            : string.Empty;
        var cacheFileName = GetCacheFileNameForRequest(
            request.Method, 
            isStream,
            request.RequestUri!.ToString(),
            requestBody);
        
        Directory.CreateDirectory(CacheDirectory);

        // Read from cache if available
        if (File.Exists(cacheFileName))
        {
            var cachedResponse = new HttpResponseMessage(System.Net.HttpStatusCode.OK) 
            {
                RequestMessage = request,
            };

            if (isStream)
            {
                // Read and stream part
                cachedResponse.Content = new StreamContent(new FileStream(cacheFileName, FileMode.Open, FileAccess.Read));
            }
            else
            {
                var cachedResponseContent = await File.ReadAllTextAsync(cacheFileName, cancellationToken);
                cachedResponse.Content = new StringContent(cachedResponseContent, Encoding.UTF8, "application/json");
            }

            return cachedResponse;
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            using var fileStream = new FileStream(cacheFileName, FileMode.Create, FileAccess.Write);

            if (isStream)
            {
                await response.Content.CopyToAsync(fileStream, cancellationToken);
            }
            else
            {
                var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
                await File.WriteAllTextAsync(cacheFileName, responseBody, cancellationToken);
            }
        }

        return response;
    }
    
    private static string GetCacheFileNameForRequest(
        HttpMethod method,
        bool isStream,
        string url,
        string body)
    {
        var source = Encoding.UTF8.GetBytes(method + isStream.ToString() + url + body);
        var hashBytes = SHA256.HashData(source);
        StringBuilder sb = new();
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }

        return Path.Combine(CacheDirectory, sb.ToString() + ".txt");
    }
}