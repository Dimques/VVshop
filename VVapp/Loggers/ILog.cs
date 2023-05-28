namespace VVapp.Loggers;

public interface ILog
{
    public ILog ForContext(string context);
    public void Success(string message);
    public void Info(string message);
    public void Warn(string message);
    public void Error(Exception error);
}