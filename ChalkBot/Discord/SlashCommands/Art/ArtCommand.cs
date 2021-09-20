using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChalkBot.Discord.Attributes;
using ChalkBot.Extensions;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using JetBrains.Annotations;

namespace ChalkBot.Discord.SlashCommands.Art
{
  [UsedImplicitly]
  [RequiredChannels(Constants.Channels.TestingChannel, Constants.Channels.ArtLobbyChannelId)]
  public class ArtCommand : ApplicationCommandModule
  {
    private readonly DiscordClient _client;

    public ArtCommand(DiscordClient discordClient)
    {
      this._client = discordClient;
    }

    [UsedImplicitly]
    [SlashCommand("art", "Заявка на добавление арта в лобби")]
    public async Task ArtRequest(InteractionContext ctx,
      [Option("server", "Сервер")] Constants.Server server,
      [Option("coords", "Координаты арта(x: 999, z: -999)")]
      string coords,
      [Option("name", "Название арта")] string artName,
      [Option("names", "Ники строителей арта, через |")]
      string builders)
    {
      var names = builders.Split('|');

      var embed = new DiscordEmbedBuilder()
        .WithTitle(artName)
        .WithDescription(String.Join(" | ", names.Select(n => $"`{n.Trim()}`")))
        .AddField("Сервер", server.GetEnumDescription(), true)
        .AddField("Координаты", coords, true)
        .WithColor(DiscordColor.Aquamarine)
        .WithFooter($"{ctx.Member.Username}#{ctx.Member.Discriminator}", ctx.Member.AvatarUrl)
        .WithTimestamp(DateTime.Now)
        .Build();
      await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
        new DiscordInteractionResponseBuilder().AddEmbed(embed));
    }
  }
}