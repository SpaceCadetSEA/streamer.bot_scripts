using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Enums;
using Streamer.bot.Plugin.Interface.Model;
using Streamer.bot.Common.Events;
using System;
using System.Text.Json;


public class QuoteData
{
    public int Id { get; set; }
    public string Text { get; set; }
    public string Game { get; set; }
    public DateTime Timestamp { get; set; }
}

// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineQuotes : CPHInlineBase
{
    public bool Execute()
    {
        // Get the quote text from chat command arguments
        CPH.GetGlobalVal("global_quoteBook", out List<QuoteData> quoteBook);
        if (quoteBook == null)
        {
            // if quoteBook is null, read it in from disk? Or create new?
        }

        // Get current game/category from Twitch broadcaster.
        var userInfoEx = CPH.TwitchGetExtendedUserInfoById(CPH.TwitchGetBroadcastUserId());
        string currentGame = userInfoEx.Game;

        // Get quote text from user input
        CPH.TryGetArg("rawInput", out string quoteText);

        // Format the quote with game


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