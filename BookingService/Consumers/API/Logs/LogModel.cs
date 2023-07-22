using Serilog;

namespace API.Logs;

public class LogModel
{

    private string MethodName { get; set; }
    private string Message { get; set; }
    private ELogType LogType { get; set; }

    public LogModel()
    {
    }


    public void RecLog(string methodName, string message, ELogType logType)
    {
        MethodName = methodName;
        Message = message;
        LogType = logType;
        RecLog();
    }

    private void RecLog()
    {
        var message = $"{MethodName} -  {Message}";
        switch(LogType)
        {
            case ELogType.LogInformation:
                Log.Information (message);
                break;
            case ELogType.LogDebug:
                Log.Debug (message); 
                break;
            case ELogType.LogWarning:
                Log.Warning (message);
                break;
            case ELogType.LogError:
                Log.Error (message);
                break;
        }
    }

}
