using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using JetBrains.Annotations;

namespace ChalkBot.Discord.SlashCommands
{
  [UsedImplicitly]
  public class Ping : ApplicationCommandModule
  {
    private readonly DiscordClient _discordClient;

    public Ping(DiscordClient discordClient)
    {
      this._discordClient = discordClient;
    }

    [UsedImplicitly]
    [SlashCommand("ping", "pong")]
    public async Task Command(InteractionContext ctx)
    {
      if (!ctx.Channel.Id.Equals(884126015956332564)) return;
      await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
        new DiscordInteractionResponseBuilder()
          .WithContent("pong"));
    }
  }
}