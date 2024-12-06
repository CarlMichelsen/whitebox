namespace Application.Mapper;

public static class TimeMapper
{
    private static readonly DateTime UnixEpochStart = new DateTime(
        1970, 
        1, 
        1, 
        0, 
        0, 
        0,
        DateTimeKind.Utc);
    
    public static long GetUnixTimeSeconds(DateTime dateTime)
    {
        return (long)(dateTime.ToUniversalTime() - UnixEpochStart).TotalMilliseconds;
    }
}