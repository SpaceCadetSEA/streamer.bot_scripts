using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Streamer.bot.Plugin.Interface;


public class CPHInline : CPHInlineBase
{
    public bool Execute()
    {
        // insert your own filepath here
        string filePath = "C:/Users/Space Cadet/stream/quoteBook.json";
        if (!File.Exists(filePath))
        {
            CPH.SendMessage("Failed to find filepath");
            return false;
        }
        // read our .json from disk and deserialize to convert to QuoteEntry objects.
        string jsonData = File.ReadAllText(filePath);
        List<QuoteEntry>? quoteBook = JsonConvert.DeserializeObject<List<QuoteEntry>>(jsonData);
        if (quoteBook is null || quoteBook.Count == 0)
        {
            CPH.SendMessage("Failed to load quote book from disk :(");
        }
        // global variables must be serialized before they can be set.
        string serializedQuoteBook = JsonConvert.SerializeObject(quoteBook);
        CPH.SetGlobalVar("global_quoteBook", serializedQuoteBook);
        return true;
    }
}

/*
    In order to use QuoteEntry, you must save your quotes as a list of .json
    objects with in the following fields: quoteId, quote, game, date.
*/ 
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

    public override string ToString()
    {
        return $"Quote #{QuoteId}: \"{Quote}\" [{Game}] [{Date.ToString("MM/dd/yyyy")}]";
    }
}