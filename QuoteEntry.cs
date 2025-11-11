using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}