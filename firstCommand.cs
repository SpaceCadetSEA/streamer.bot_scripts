using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineFirst : CPHInlineBase
{
    public bool Execute()
    {
        List<KeyValuePair<string, int>> topUsers = CPH.GetGlobalVar<List<KeyValuePair<string, int>>>("global_currentTopFive");
        if (topUsers == null)
        {
            CPH.SendMessage("Top 5 not found!");
        }
        else
        {
            CPH.SendMessage("🏆 MO and Marie's favorites 🏆");
            for (int i = 0; i < topUsers.Count; i++)
            {
                int rank = i + 1;
                KeyValuePair<string, int> pair = topUsers[i];
                string userName = pair.Key;
                int firstCount = pair.Value;
                CPH.SendMessage($"#{rank}) {userName} - {firstCount}");
            }
        }
        return true;
    }
}