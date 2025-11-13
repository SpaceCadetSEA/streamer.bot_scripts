using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

/**
    {
      "timestamp": "2025-11-09T21:25:54.9211552-08:00",
      "id": 1,
      "userId": "531894843",
      "user": "SpaceCadetSEA",
      "platform": "twitch",
      "gameName": "SILENT HILL ƒ",
      "quote": "this is another test"
    },
*/


namespace StreamerBotQuotes
{
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

        public Dictionary<string, dynamic> ToJsonObject()
        {
            return new Dictionary<string, dynamic>{
                { "timestamp", $"{Date:YYYY-MM-DDTHH:mm:ss.sss±HH:mm}" },
                { "id", QuoteId },
                { "userId", "" },
                { "user", "" },
                { "platform", "twitch"},
                { "gameName", $"{Game}"},
                { "quote", $"{quote}"}
            };
        }
    }
}