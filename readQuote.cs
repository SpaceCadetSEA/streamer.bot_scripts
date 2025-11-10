using System;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Model;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineReadQuote : CPHInlineBase
{
    public bool Execute()
    {
        // Get the quote text from chat command arguments
        List<QuoteData> quoteBook = CPH.GetGlobalVar<List<QuoteData>>("global_quoteBook");
        if (quoteBook == null)
        {
            CPH.SendMessage("Quotes are unavailable.");
            return false;
        }
        int quoteIndex = GetQuoteIndex(quoteBook.Count);
        QuoteData quote = quoteBook[quoteIndex];
        CPH.SendMessage($"#{quoteIndex + 1} | {quote.Quote}");
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