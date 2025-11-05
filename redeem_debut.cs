
public class CPHInline
{
    public bool Execute()
    {
        CPH.TryGetArg("rewardId", out string rewardId);
        CPH.LogInfo($"Reward ID: {rewardId}");
        CPH.SendMessage($"Reward ID logged to Streamer.bot console");
        return true;
    }
}