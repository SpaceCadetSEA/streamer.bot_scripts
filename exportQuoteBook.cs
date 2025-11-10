using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Streamer.bot.Plugin.Interface;
using System.Text.Json.Serialization;

public class CPHInline : CPHInlineBase
{
    public bool Execute()
    {
        // insert your own filepath here
        string filePath = "C:/Users/Space Cadet/stream/quoteBook.json";
        List<QuoteEntry> quoteBook = CPH.GetGlobalVar<List<QuoteEntry>>("global_quoteBook");
        string quoteBookJson = JsonConverter.SerializeObject(quoteBook);
        File.WriteAllText(quoteBookJson);

        return true;
    }
}
