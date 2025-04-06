using static CPHmock.LogLevels;
using static MockConstants;

public static class MockConstants
{
    public const string MockAppName = "[MOCK] ";

}

public class CPHmock : IInlineInvokeProxy
{
    private const LogLevels DefaultLogLevel = Info;
    private const LogLevels DefaultLogLevelSB = Debug;

    private static LogLevels LogLevel;
    private static LogLevels LogLevelSB;
    private string? currentScene;

    public static Dictionary<string, object> args;

    private static Dictionary<string, Tuple<string, String[]>> triggers;

    public enum LogLevels
    {
        Verbose,
        Debug,
        Info,
        Warn,
        Error_
    }

    public CPHmock()
    {
     
        triggers = new Dictionary<string, Tuple<string, String[]>>();


     


}

    public void LogWarn(string str)
    {
        if (LogLevelSB <= Warn) Console.WriteLine(Now("WRN") + str);
    }

    public void LogInfo(string str)
    {
        if (LogLevelSB <= Info) Console.WriteLine(Now("INF") + str);
    }

    public void LogDebug(string str)
    {
        if (LogLevelSB <= Debug) Console.WriteLine(Now("DBG") + str);
    }

    public void LogVerbose(string str)
    {
        if (LogLevelSB <= Verbose) Console.WriteLine(Now("VER") + str);
    }

    private static string Now(string logLevel)
    {
        return "[ " + DateTime.Now + " " + logLevel + "] ";
    }

    private static LogLevels GetLogLevel(string? level)
    {
        if (level == null) return DefaultLogLevel;
        return level switch
        {
            "VERBOSE" => Verbose,
            "DEBUG" => Debug,
            "INFO" => Info,
            "WARN" => Warn,
            "ERROR" => Error_,
            _ => DefaultLogLevel
        };
    }

    public bool ObsIsConnected(int connection = 0)
    {
        return true;
    }

    public void ObsSetScene(string str)
    {
        Console.WriteLine(MockAppName + $"Setting OBS scene to: {str}");
        currentScene = str;
    }

    public string ObsGetCurrentScene()
    {
        var obsGetCurrentScene = currentScene;
        //Console.WriteLine(MockAppName + $"OBS current scene is: {obsGetCurrentScene}");
        return obsGetCurrentScene;
    }

    public bool SlobsIsConnected(int connection = 0)
    {
        return false;
    }

    public void SlobsSetScene(string str)
    {
        Console.WriteLine(MockAppName + $"Setting SLOBS scene to {str}");
    }

    public string SlobsGetCurrentScene()
    {
        return currentScene;
    }

    public void SendMessage(string str, bool bot = true)
    {
        Console.WriteLine(MockAppName + $"SendMessage: {str}");
    }

    public void SendYouTubeMessage(string str, bool bot = true)
    {

    }

    public void RunAction(string str, bool runImmediately = true)
    {
        string when = runImmediately ? "immediately" : "consecutively";
        Console.WriteLine(MockAppName + $"Running action: {str} {when}");
    }


    public string? GetGlobalVar<Type>(string key, bool persisted = true)
    {
        var value = key switch
        {
            _ => (string?)null
        };

        if (value == null) Console.WriteLine($"{MockAppName}Key {key} is not found!");

        return value;
    }


    public void SetGlobalVar(string varName, object value, bool persisted = true)
    {
        if (LogLevel <= Verbose)
        {
            Console.WriteLine(
                MockAppName + string.Format("Writing value {1} to variable {0}", varName, value));
        }
    }

    public void UnsetGlobalVar(string varName, bool persisted = true)
    {
        Console.WriteLine(MockAppName + $"UnsetGlobalVar var: {varName}");
    }
    
    public bool RegisterCustomTrigger(string triggerName, string eventName, String[] categories)
    {
        if (!triggers.ContainsKey(eventName))
        {
            triggers.Add(eventName, new Tuple<string, string[]>(triggerName, categories));
            return true;
        }
        return false;
    }

    public void TriggerCodeEvent(string eventName, bool useArgs = true)
    {

    }
    public void TriggerCodeEvent(string eventName, Dictionary<string, object> args)
    {

    }

    public bool TryGetArg<T>(string argName, out T value)
    {
        if (args.ContainsKey(argName))
        {
            value = (T)args[argName];
            return true;
        }
        value = default(T);
        return false;
    }

    public bool TryGetArg(string argName, out object value)
    {
        if (args.ContainsKey(argName))
        {
            value = args[argName];
            return true;
        }
        value = null;
        return false;
    }
   
    public void Wait(int milliseconds)
    {
        Thread.Sleep(milliseconds);
    }

    public static void Main(string[] args)
    {
        CPHInline cphInline = new CPHInline();

        cphInline.Init();

        while (true)
        {
            cphInline.Execute();
            Thread.Sleep(1000);

        }
    }

    private record Config
    {
        public string? snifferIp { get; set; }
        public string? snifferPort { get; set; }
        public string? menuScene { get; set; }
        public string? songScenes { get; set; }
        public string? pauseScene { get; set; }
        public string? blackList { get; set; }
        public string? behavior { get; set; }
        public string? switchScenes { get; set; }
        public string? songSceneAutoSwitchMode { get; set; }
        public string? sceneSwitchPeriod { get; set; }
        public string? sceneSwitchCooldownPeriod { get; set; }
        public string? sectionActions { get; set; }
        public string? logLevel { get; set; }
        public string? logLevelSB { get; set; }
       
    }
}