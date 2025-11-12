using Newtonsoft.Json;
using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Model;
using System;
using System.Linq;
using System.IO;


public class CPHInlineAddQuote : CPHInlineBase
{
    public bool Execute()
    {
        // Get quote text from user input
        CPH.TryGetArg("rawInput", out string quote);

        // Get current game/category from Twitch broadcaster.
        var userInfoEx = CPH.TwitchGetExtendedUserInfoById(CPH.TwitchGetBroadcaster().UserId);
        string game = userInfoEx.Game;

        CPH.TryGetArg("nextQuoteId", out int quoteId);
        QuoteEntry newQuote = new QuoteEntry(quoteId, quote, game, DateTime.Now);

        // write quote to Quotes.txt
        if (CPH.TryGetArg("quoteFilePath", out string quoteFilePath))
        {
            File.AppendAllText(quoteFilePath, newQuote.ToQuoteTextFile() + Environment.NewLine);
            CPH.SendMessage($"Quote added! {newQuote.ToString()}");
            CPH.SetArgument("nextQuoteId", quoteId + 1);
        }
        else
        {
            CPH.SendMessage("Quote unable to be added at this time. :(");
        }
        return true;
    }
}

public class QuoteEntry
{
    public int QuoteId;
    public string Quote;
    public string Game;
    public DateTime Date;

    public QuoteEntry(int quoteId, string quote, string game, DateTime date)
    {
        QuoteId = quoteId;
        Quote = quote;
        Game = game;
        Date = date;
    }

    public QuoteEntry(string rawText)
    {
        List<string> textFields = rawText.Split('\t').ToList();
        QuoteId = int.Parse(textFields[0]);
        Quote = textFields[1];
        Game = textFields[2];
        Date = DateTime.Parse(textFields[3]);
    }

    public override string ToString()
    {
        return $"Quote #{QuoteId}: \"{Quote}\" [{Game}] [{Date:MM/dd/yyyy}]";
    }

    public string ToQuoteTextFile()
    {
        return $"{QuoteId}\t{Quote}\t{Game}\t{Date}";
    }
}