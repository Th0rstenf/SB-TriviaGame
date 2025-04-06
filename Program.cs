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

    private static readonly HttpClient client = new HttpClient();

    public static async Task<string> Get(string url)
    {
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    enum TriviaState
    {
        Waiting,
        FetchingQuestion,
        Answering,
        Finished
    }

    private TriviaState itsState = TriviaState.FetchingQuestion;
    private string itsSessionToken = "";
    private void AcquireSessionToken()
    {
        string command = "https://opentdb.com/api_token.php?command=request";
        itsSessionToken = Get(command).Result;
    }
    private void FetchQuestion()
    {
        string url = "https://opentdb.com/api.php?amount=1&type=multiple";
        string json = Get(url).Result;
        CPH.SendMessage($"Trivia question fetched: {json}");
    }
    public void Init()
    {
        AcquireSessionToken();
    }

    public bool Execute()
    {
        switch (itsState)
        {
            case TriviaState.Waiting:
                //Nothing to do
                break;
            case TriviaState.FetchingQuestion:
                CPH.SendMessage("Fetching trivia question...");
                FetchQuestion();
                itsState = TriviaState.Answering;
                break;
            case TriviaState.Answering:

                itsState = TriviaState.Finished;
                break;
            case TriviaState.Finished:
                itsState = TriviaState.Waiting;
                break;
        }
        CPH.SendMessage("Starting trivia game!");

        return true;
    }

    public void Dispose()
    {

    }
}