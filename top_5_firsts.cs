using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Streamer.bot.Common.Events;
using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Enums;
using Streamer.bot.Plugin.Interface.Model;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHFirstUsers : CPHInlineBase
{
    public bool Execute()
    {
        CPH.TryGetArg("userName", out string userName);
        CPH.TryGetArg("userCounter", out int userCounter);
        // read in the firsts dictionary object from global variable?
        // CPH.TryGetArg("global_viewerFirstCount", out Dictionary<string, int> viewerFirstCount);
        Dictionary<string, int> viewerFirstCount = CPH.GetGlobalVar<Dictionary<string, int>>("global_viewerFirstCount");
        // try to add user to counter
        if (viewerFirstCount == null)
        {
            viewerFirstCount = new Dictionary<string, int>();
        }
        // Overwrite existing userCounter (first redeem) for user
        viewerFirstCount[userName] = userCounter;
        CPH.SetGlobalVar("global_viewerFirstCount", viewerFirstCount);

        // Generate top 5 users from first redeem
        List<KeyValuePair<string, int>> topUsers = [.. viewerFirstCount];
        topUsers.Sort((x, y) => x.Value);
        // limit to 5
        topUsers.GetRange(0, 5);
        CPH.SetGlobalVar("global_currentTopFive", topUsers);

        return true;
    }
}
