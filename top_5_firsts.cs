using Streamer.bot.Plugin.Interface;
using Streamer.bot.Plugin.Interface.Enums;
using Streamer.bot.Plugin.Interface.Model;
using Streamer.bot.Common.Events;
using System;


// Must update class name to CPHInline when porting over to Streamer.bot C# sub-action
public class CPHInline : CPHInlineBase
{
    public bool Execute()
    {
        // Get the reward ID you want to track
        // You can find this in Streamer.bot or pass it as an argument
        string rewardId = "your-reward-id-here";

        // APPROACH 1: Using Streamer.bot's built-in tracking
        // Note: Only tracks redemptions Streamer.bot has seen

        // Get all users from your user database
        var users = CPH.TwitchGetUsers();

        // Create a list to store user redemption counts
        var userRedemptions = new List<UserRedemptionData>();

        foreach (var user in users)
        {
            // Get the redemption count for this user
            var counter = CPH.TwitchGetRewardUserCounterById(user.UserId, rewardId, true);

            if (counter != null && counter.Counter > 0)
            {
                userRedemptions.Add(new UserRedemptionData
                {
                    Username = user.UserLogin,
                    Count = counter.Counter
                });
            }
        }

        // Get top 5 users (LINQ - similar to JS .sort().slice())
        var top5 = userRedemptions
            .OrderByDescending(u => u.Count)  // Sort by count descending
            .Take(5)                          // Take first 5
            .ToList();

        // Format the output
        string message = "🏆 Top 5 Redeemers:\n";
        for (int i = 0; i < top5.Count; i++)
        {
            message += $"{i + 1}. {top5[i].Username}: {top5[i].Count} redemptions\n";
        }

        // Send to chat (or wherever you want)
        CPH.SendMessage(message);

        // OR: Return as a variable for use in other sub-actions
        CPH.SetArgument("top5Users", top5);

        return true;
    }
}

public class UserRedemptionData
{
    public string Username { get; set; }
    public int Count { get; set; }
}