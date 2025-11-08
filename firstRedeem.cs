using System.Collections.Generic;
using Streamer.bot.Plugin.Interface;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHFirstUsers : CPHInlineBase
{
    public bool Execute()
    {
        CPH.TryGetArg("userName", out string userName);
        CPH.TryGetArg("userCounter", out int userCounter);

        Dictionary<string, int> viewerFirstCount = CPH.GetGlobalVar<Dictionary<string, int>>("global_viewerFirstCount");
        // Fall back if the global variable does not yet exist.
        viewerFirstCount ??= new Dictionary<string, int>();

        // Overwrite existing userCounter (first redeem) for user
        viewerFirstCount[userName] = userCounter;
        CPH.SetGlobalVar("global_viewerFirstCount", viewerFirstCount);

        // Generate top 5 users from first redeem
        // By placing this here, we will only run this code once per stream.
        // This could be an expensive operation, but even with 10,000 individual
        // entries in the viewerFirstCount dictionary, this should be performant
        // enough.
        List<KeyValuePair<string, int>> topUsers = [.. viewerFirstCount];
        topUsers.Sort((x, y) => x.Value);
        // limit to 5
        topUsers.GetRange(0, 5);
        CPH.SetGlobalVar("global_currentTopFive", topUsers);

        return true;
    }
}
