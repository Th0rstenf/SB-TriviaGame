using System;


public class CPHInline
{
    // -------------------------------------------------
    // Needs to be commented out in streamer bot!
    private CPHmock CPH = new CPHmock();
    private Dictionary<string, object> args = CPHmock.args;
    // -------------------------------------------------

    public struct Constants
    {
        public const string AppName = "SB_Trivia";
    }

    public void Init()
    {

    }

    public bool Execute()
    {
        return true;
    }

    public void Dispose()
    {

    }
}