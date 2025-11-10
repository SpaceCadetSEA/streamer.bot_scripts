using Newtonsoft.Json;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Model;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineAddQuote : CPHInlineBase
{
    public bool Execute()
    {
        // Not sure if this will be deserialized or if we need to
        List<QuoteEntry> quoteBook = CPH.GetGlobalVar<List<QuoteEntry>>("global_quoteBook");
        if (quoteBook == null)
        {
            quoteBook = [];
        }
        int quoteId = quoteBook.Count;

        // Get quote text from user input
        CPH.TryGetArg("rawInput", out string quote);

        // Get current game/category from Twitch broadcaster.
        var userInfoEx = CPH.TwitchGetExtendedUserInfoById(CPH.TwitchGetBroadcaster().UserId);
        string game = userInfoEx.Game;

        DateTime date = DateTime.Now();

        QuoteEntry newQuote = QuoteEntry(quoteId, quote, game, date);
        quoteBook.Add(newQuote);

        CPH.SendMessage($"Quote added! {newQuote.ToString()}");

        string serializedQuoteBook = JsonConvert.SerializeObject(quoteBook);
        CPH.SetGlobalVar("global_quoteBook", serializedQuoteBook);
        return true;
    }
}