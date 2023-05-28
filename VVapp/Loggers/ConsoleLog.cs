namespace VVapp.Loggers;

public class ConsoleLog : ILog
{
    private string? _context;

    public ILog ForContext(string context)
    {
        _context = EncloseWithBrackets(context);
        return this;
    }

    public void Info(string message) => Log(message, LogLevel.Info);

    public void Warn(string message) => Log(message, LogLevel.Warn);

    public void Error(Exception exception) => Log(exception.Message, LogLevel.Error);
    private void Log(string message, LogLevel level)
    {
        var dateTime = EncloseWithBrackets(DateTime.Now.ToLongTimeString());
        var logLevel = EncloseWithBrackets(level.ToString());

        Console.ForegroundColor = GetLevelColor(level);
        Console.WriteLine(dateTime + " " + logLevel + " " + (_context ?? string.Empty) + " " + message);
        Console.ForegroundColor = ConsoleColor.White;
    }

    private static string? EncloseWithBrackets(string? message)
        => message is not null ? "[" + message + "]" : message;

    private ConsoleColor GetLevelColor(LogLevel level)
    {
        return level switch
        {
            LogLevel.Warn => ConsoleColor.Yellow,
            LogLevel.Error => ConsoleColor.Red,
            _ => ConsoleColor.White
        };
    }

    private enum LogLevel
    {
        Info = 0,
        Warn = 1,
        Error = 2
    }
}