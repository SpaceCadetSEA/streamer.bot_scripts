# streamer.bot_scripts

*It is crucial that the class name be updated in each of these scripts to be CPHInline. I have changed the names in order to develop multiple C# scripts locally.*
EX: CPHInlineQuotes -> CPHInline

NOTE: If it is easier, feel free to just copy the code within the Execute method and past into a new instance of the C# Execute Code sub-action.

## Current C# Scripts

`firstCommand.cs`
- Simple script for returning the top 5 users who have redeemed your first command.
- Uses a global variable called `global_currentTopFive` (`List<KeyValuePair<string, int>>`) to track the top 5.
- Needs to work in conjunction with firstRedeem.cs, which creates the global variable.

`firstRedeem.cs`
- Script that runs after a user selects the First redeem from Twitch channel rewards (or other triggers).
- This will use the built in arguments for userName (the redeemer) and userCount (number of times the user has redeemed the specific redeem) in order to catalog each user who redeems the first command.
- This will write a new global variable called `global_viewerFirstCount` (`Dictionary<string, int>`) to maintain our counter of first redeems.
- This will also generate our global_currentTopFive variable by using the updated viewer count dictionary to generate the list of the top 5 first redeemers in descending order.
- This method should be plenty performant for dictionaries up to 10,000 users.
