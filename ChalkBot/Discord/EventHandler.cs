using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

namespace ChalkBot.Discord
{
  public class EventHandler
  {
    private readonly DiscordClient _discordClient;
    private const ulong IdeasChannel = 874997770963009546;
    private const ulong AdBoardChannel = 878504975469273130;
    private const ulong TestChannel = 884126015956332564;

    public EventHandler(DiscordClient discordClient)
    {
      this._discordClient = discordClient;

      this._discordClient.MessageCreated += this.MessageCreated;
      this._discordClient.ComponentInteractionCreated += this.OnInteraction;
    }

    private async Task<Task> MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
      if (new List<ulong> { IdeasChannel, TestChannel }.Any(x => x == args.Channel.Id))
      {
        // var startPosition = args.Message.Content.IndexOf('-') + 1;
        //
        // if (!args.Message.Content.StartsWith('-')) return Task.CompletedTask;
        // var content = args.Message.Content[startPosition..];
        //
        // var msgBuilder = new DiscordMessageBuilder();
        //
        // var embed = new DiscordEmbedBuilder()
        //   .WithColor(new DiscordColor(0xDB8384))
        //   .WithAuthor($"Предложение от {args.Author.Username}#{args.Author.Discriminator}", null, args.Author.AvatarUrl)
        //   .WithDescription(content)
        //   .WithTimestamp(DateTime.Now);
        //
        // await args.Message.DeleteAsync();
        // var msg = await args.Channel.SendMessageAsync(
        //   embed
        //     .WithFooter($"User ID: {args.Author.Id} | Message ID: %MsgId%")
        //     .Build());
        //
        // await msg.ModifyAsync(
        //   embed
        //     .WithFooter($"User ID: {args.Author.Id} | Message ID: {msg.Id}")
        //     .Build()
        // );
        //
        // return Task.CompletedTask;
      }
      else if (new List<ulong> { AdBoardChannel, TestChannel }.Any(x => x == args.Channel.Id))
      {
        if ((args.Author.IsBot || this._discordClient.CurrentApplication.Owners.Any(x => x.Id == args.Author.Id)) &&
            !args.Channel.Id.Equals(TestChannel))
          return Task.CompletedTask;
        await args.Message.DeleteAsync();
      }

      return Task.CompletedTask;
    }

    private async Task<Task> OnInteraction(DiscordClient sender, ComponentInteractionCreateEventArgs interaction)
    {
      if (interaction.Message.Embeds[0].Footer is null) return Task.CompletedTask;
      if (Convert.ToUInt64(interaction.Message.Embeds[0].Footer.Text) != interaction.User.Id)
      {
        await interaction.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
          new DiscordInteractionResponseBuilder()
            .WithContent("Вы не владеете объявлением")
            .AsEphemeral(true));
        return Task.CompletedTask;
      }

      if (interaction.Interaction.Data.CustomId != "delete_ad") return Task.CompletedTask;
      await interaction.Message.DeleteAsync();

      return Task.CompletedTask;
    }
  }
}