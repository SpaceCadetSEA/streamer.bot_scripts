using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Streamer.bot.Plugin.Interface;


/*
Current flow:
    1. export from MIU
    2. run python script to convert .txt file to JSON
    3. run THIS file, importQuoteBook.cs, in Streamer.bot
    4. Saves quotes as global variable, string.
    5. Need to serialize and deserialize each time

New flow:
    1. export from MIU
    2. custom QuoteEntry class
    3. use sub-actions read random line from file or read line from file
    4. KEEP the Quotes.txt from MIU
        a. all random quote reads:
            i. read a random line from Quotes.txt
            ii. convert to QuoteEntry object
            iii. sendMessage with QuoteEntry.ToString()
        b. direct quote:
            i. read specific line from Quotes.txt
            ii. convert to QuoteEntry object
            iii. sendMessage with QuoteEntry.ToString()
    5. ADDING new quotes
        a. parse rawInput convert to QuoteEntry
        b. write (append) to Quotes.txt tab separated 
            # \t Quote \t Game \t Date/Time

*/ 

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