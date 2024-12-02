using System.Security.Cryptography;
using System.Text;
using LLMIntegration.Util;

namespace Test.Fake;

public class TestReplayHttpDelegatingHandler : DelegatingHandler
{
    private readonly string cacheDirectory = Path.Combine(GetProjectRootDirectory(), "TestData/TestResponseCache");
    
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var isStream = request.Headers.TryGetValues(LlmConstants.LlmIsStreamHeaderName, out _);
        var requestBody = request.Content != null
            ? await request.Content.ReadAsStringAsync(cancellationToken)
            : string.Empty;
        var cacheFileName = this.GetCacheFileNameForRequest(
            request.Method,
            isStream,
            request.RequestUri!,
            requestBody);
        
        Directory.CreateDirectory(this.cacheDirectory);

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
                cachedResponse.Content = new StreamContent(
                    new FileStream(cacheFileName, FileMode.Open, FileAccess.Read));
            }
            else
            {
                var cachedResponseContent = await File.ReadAllTextAsync(
                    cacheFileName,
                    cancellationToken);
                
                cachedResponse.Content = new StringContent(
                    cachedResponseContent,
                    Encoding.UTF8,
                    "application/json");
            }

            return cachedResponse;
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return response;
        }
        
        if (isStream)
        {
            await using var fileStream = new FileStream(
                cacheFileName,
                FileMode.Create,
                FileAccess.Write);
            
            // TODO: http-stream tests will fail on first run to write file.
            await response.Content.CopyToAsync(fileStream, cancellationToken);
        }
        else
        {
            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            await File.WriteAllTextAsync(cacheFileName, responseBody, cancellationToken);
        }

        return response;
    }
    
    private static string GetProjectRootDirectory()
    {
        var dir = Directory.GetCurrentDirectory();

        while (Directory.GetFiles(dir, "*.csproj").Length == 0)
        {
            dir = Directory.GetParent(dir)!.FullName;
        }

        return dir;
    }
    
    private string GetCacheFileNameForRequest(
        HttpMethod method,
        bool isStream,
        Uri uri,
        string body)
    {
        var source = Encoding.UTF8.GetBytes(method + isStream.ToString() + uri.GetLeftPart(UriPartial.Path) + body);
        var hashBytes = SHA1.HashData(source);
        StringBuilder sb = new(uri.Host + "_");
        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("x2"));
        }
        
        sb.Append(".txt");
        return Path.Combine(this.cacheDirectory, sb.ToString());
    }
}