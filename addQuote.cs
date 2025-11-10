using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Model;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineAddQuote : CPHInlineBase
{
    public bool Execute()
    {
        // Get the quote text from chat command arguments
        List<QuoteData> quoteBook = CPH.GetGlobalVar<List<QuoteData>>("global_quoteBook");
        if (quoteBook == null)
        {
            quoteBook = [];
        }
        int currentQuoteId = quoteBook.Count;

        // Get current game/category from Twitch broadcaster.
        var userInfoEx = CPH.TwitchGetExtendedUserInfoById(CPH.TwitchGetBroadcaster().UserId);
        string currentGame = userInfoEx.Game;

        // Get quote text from user input
        CPH.TryGetArg("rawInput", out string quoteText);

        // Format the quote with game

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

        // Add new quote
        quotes.Add(new QuoteData
        {
            Id = quotes.Count + 1,
            Quote = quoteText,
            GameName = currentGame,
            User = author,
            Timestamp = DateTime.Now
        });

        // Send confirmation to chat
        CPH.SendMessage($"Quote #{quotes.Count} added: {formattedQuote}");

        return true;
    }
}