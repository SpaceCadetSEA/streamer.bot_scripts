using System;

public class CPHInline
{
    public bool Execute()
    {
        // Get the quote text from chat command arguments
        // When user types: !addquote This is my quote
        CPH.TryGetArg("rawInput", out string quoteText);

        // Get current game from Twitch
        string currentGame = CPH.TwitchGetGameName();

        // Alternative: Get from user info
        // var userInfo = CPH.TwitchGetExtendedUserInfoById(CPH.TwitchGetBroadcastUserId());
        // string currentGame = userInfo.Game;

        // Get username who created the quote
        CPH.TryGetArg("userName", out string author);

        // Format the quote with game
        string formattedQuote = $"\"{quoteText}\" - while playing {currentGame}";

        // Store the quote (you'll need to decide how to persist this)
        // Option 1: Use Streamer.bot's built-in quote system
        // Option 2: Store in a global variable with array
        // Option 3: Write to a file

        // Example using global variable (simple approach):
        if (!CPH.TryGetArg("quoteId", out int quoteId))
        {
            quoteId = 1; // Start at 1 if no quotes exist
        }

        // Get existing quotes list or create new one
        string quotesJson = CPH.GetGlobalVar<string>("allQuotes", true) ?? "[]";
        var quotes = System.Text.Json.JsonSerializer.Deserialize<List<QuoteData>>(quotesJson) ?? new List<QuoteData>();

        // Add new quote
        quotes.Add(new QuoteData
        {
            Id = quotes.Count + 1,
            Text = quoteText,
            Game = currentGame,
            Author = author,
            Timestamp = DateTime.Now
        });

        // Save back to global variable
        string updatedJson = System.Text.Json.JsonSerializer.Serialize(quotes);
        CPH.SetGlobalVar("allQuotes", updatedJson, true);

        // Send confirmation to chat
        CPH.SendMessage($"Quote #{quotes.Count} added: {formattedQuote}");

        return true;
    }
}

// Helper class for quote data
public class QuoteData
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Game { get; set; }
    public string Author { get; set; }
    public DateTime Timestamp { get; set; }
}