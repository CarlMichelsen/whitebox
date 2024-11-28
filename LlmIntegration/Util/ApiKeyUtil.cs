namespace LLMIntegration.Util;

public static class ApiKeyUtil
{
    public static string GetRandomKey(List<string> keys)
    {
        var random = Random.Shared;
        var index = random.Next(keys.Count - 1);
        return keys[index];
    }
}