namespace KForceCars.Services;

public static class ServiceExtensions
{
    public static bool IsBetween(this decimal value, decimal initialValue, decimal finalValue)
        => value >= initialValue && value <= finalValue;
}