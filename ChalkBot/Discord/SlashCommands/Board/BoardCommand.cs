using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ChalkBot.Discord.Attributes;
using ChalkBot.Extensions;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using JetBrains.Annotations;

namespace ChalkBot.Discord.SlashCommands.Board
{
  [UsedImplicitly]
  [SlashCommandGroup("board", "Доска объявлений")]
  [RequiredChannels(Constants.Channels.AdBoardChannelId, Constants.Channels.TestingChannel)]
  public class BoardCommand : ApplicationCommandModule
  {
    private readonly DiscordClient _client;

    public BoardCommand(DiscordClient client)
    {
      this._client = client;
    }

    public enum AdType
    {
      [Description("Работа")] [ChoiceName("work")]
      Work,

      [Description("Продажа")] [ChoiceName("sell")]
      Sell,

      [Description("Покупка")] [ChoiceName("buy")]
      Buy
    }

    [UsedImplicitly]
    [SlashCommand("create", "Создать объявление")]
    public async Task CreateBoard(InteractionContext ctx,
      [Option("type", "Тип объявления")] AdType adType,
      [Option("server", "Сервер")] Constants.Server server,
      [Option("description", "Описание")] string description,
      [Option("price", "Цена")] string price)
    {
      var embed = new DiscordEmbedBuilder()
        .WithColor(new DiscordColor(0xFF9D00))
        .WithAuthor($"{ctx.Member.Username}#{ctx.Member.Discriminator}", null, ctx.Member.AvatarUrl)
        .WithTitle("Объявление")
        .WithDescription(description)
        .AddField("Сервер", server.GetEnumDescription(), true)
        .AddField("Цена", price, true)
        .AddField("Тип", adType.GetEnumDescription(), true)
        .WithFooter(ctx.User.Id.ToString())
        .WithTimestamp(DateTime.Now);

      var msg = new DiscordInteractionResponseBuilder()
        .AddEmbed(embed.Build())
        .AddComponents(
          new DiscordButtonComponent(ButtonStyle.Danger, "delete_ad", null, false,
            new DiscordComponentEmoji("🗑️"))
        );

      await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, msg);
    }
  }
}