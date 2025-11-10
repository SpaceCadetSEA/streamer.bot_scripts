using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Streamer.bot.Plugin.Interface;


public class CPHInline : CPHInlineBase
{
    public bool Execute()
    {
        string filePath = "C:/Users/Space Cadet/stream/quoteBook.json";
        if (!File.Exists(filePath))
        {
            CPH.SendMessage("Failed to find filepath");
            return false;
        }

        string jsonData = File.ReadAllText(filePath);
        List<QuoteEntry> ?quoteBook = JsonConvert.DeserializeObject<List<QuoteEntry>>(jsonData);
        if (quoteBook is null || quoteBook.Count == 0)
        {
            CPH.SendMessage("Failed to load quote book from disk :(");
        }
        string serializedQuoteBook = JsonConvert.SerializeObject(quoteBook);
        CPH.SetGlobalVar("global_quoteBook", serializedQuoteBook);
        return true;
    }
}

public class QuoteEntry
{
	public int QuoteId;
	public string Quote;
	public string Game;
	public DateTime Date;

	public QuoteEntry(int quoteId, string quote, string game, DateTime date) {
		QuoteId = quoteId;
		Quote = quote;
		Game = game;
		Date = date;
	}

	public override string ToString() {
		return $"Quote #{QuoteId}: \"{Quote}\" [{Game}] [{Date.ToString("MM/dd/yyyy")}]";
	}
}