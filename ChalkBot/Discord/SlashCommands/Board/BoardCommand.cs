using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ChalkBot.Extensions;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using JetBrains.Annotations;

namespace ChalkBot.Discord.SlashCommands.Board
{
  [UsedImplicitly]
  [SlashCommandGroup("board", "Доска объявлений")]
  public class BoardCommand : ApplicationCommandModule
  {
    private readonly DiscordClient _discordClient;

    public BoardCommand(DiscordClient discordClient)
    {
      this._discordClient = discordClient;
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

    public enum Server
    {
      [Description("Maria")] [ChoiceName("maria")]
      Maria,

      [Description("Shiganshina")] [ChoiceName("shiganshina")]
      Shiganshina,

      [Description("Rose")] [ChoiceName("rose")]
      Rose,

      [Description("Sina")] [ChoiceName("sina")]
      Sina,
    }

    [UsedImplicitly]
    [SlashCommand("create", "Создать объявление")]
    public async Task CreateBoard(InteractionContext ctx,
      [Option("type", "Тип объявления")] AdType adType,
      [Option("server", "Сервер")] Server server,
      [Option("description", "Описание")] string description,
      [Option("price", "Цена")] string price)
    {
      if (!new List<ulong> { 878504975469273130, 884126015956332564 }.Contains(ctx.Channel.Id))
      {
        await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
          new DiscordInteractionResponseBuilder()
            .WithContent($"Тут не работает {DiscordEmoji.FromGuildEmote(this._discordClient, 875034683371577364)}")
            .AsEphemeral(true)
        );
        return;
      }

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