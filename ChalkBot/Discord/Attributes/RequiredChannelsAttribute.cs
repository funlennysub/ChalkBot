using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace ChalkBot.Discord.Attributes
{
  public class RequiredChannelsAttribute : SlashCheckBaseAttribute
  {
    public IReadOnlyList<ulong> RequiredChannels { get; }

    public RequiredChannelsAttribute(params ulong[] channelIds)
    {
      this.RequiredChannels = new ReadOnlyCollection<ulong>(channelIds);
    }

    public override async Task<bool> ExecuteChecksAsync(InteractionContext ctx)
    {
      var inRequiredChannel = this.RequiredChannels.Contains(ctx.Channel.Id);

      if (inRequiredChannel) return true;
      await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
        new DiscordInteractionResponseBuilder()
          .WithContent(
            $"Тут не работает {DiscordEmoji.FromGuildEmote(ctx.Client, 875034683371577364)}")
          .AsEphemeral(true)
      );
      return false;
    }
  }
}