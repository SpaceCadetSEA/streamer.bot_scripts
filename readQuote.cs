using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Model;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineReadQuote : CPHInlineBase
{
    public bool Execute()
    {
        // Read global_quoteBook
        // TODO: I'm unsure if this will be deserialized or if I need to manually deserialize.
        List<QuoteEntry> quoteBook = CPH.GetGlobalVar<List<QuoteEntry>>("global_quoteBook");
        if (quoteBook == null)
        {
            CPH.SendMessage("Quotes are unavailable.");
            return false;
        }
        int quoteIndex = GetQuoteIndex(quoteBook.Count);
        QuoteData quote = quoteBook[quoteIndex];
        return true;
    }

    private int GetQuoteIndex(int quoteBookSize)
    {
        CPH.TryGetArg<string>("rawInput", out string rawIndex);
        if (int.TryParse(rawIndex, out int quoteIndex))
        {
            return quoteIndex;
        }
        else
        {
            Random rand = new Random();
            return rand.Next(0, quoteBookSize);
        }
    }
}