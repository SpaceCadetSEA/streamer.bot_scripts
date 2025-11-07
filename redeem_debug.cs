using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Enums;
using Streamer.bot.Plugin.Interface.Model;
using Streamer.bot.Common.Events;
using System;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInlineRedeemDebut : CPHInlineBase
{
    public bool Execute()
    {
        CPH.TryGetArg("rewardId", out string rewardId);
        CPH.LogInfo($"Reward ID: {rewardId}");
        CPH.SendMessage($"Reward ID logged to Streamer.bot console");
        return true;
    }
}