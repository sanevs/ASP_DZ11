namespace Catalog;

public class UTC_Time : ICurrentTime
{
    public TimeOnly GetTime() => TimeOnly.FromDateTime(DateTime.UtcNow);
}