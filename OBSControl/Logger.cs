using MelonLoader;

namespace OBSControl;

public static class Logger
{
    internal static MelonLogger.Instance Logs;
    public static void Msg(string msg)
    {
        Logs.Msg(msg);
    }
    
    public static void Warn(string msg)
    {
        Logs.Warning(msg);
    }
    
    public static void Error(string msg)
    {
        Logs.Error(msg);
    }
}