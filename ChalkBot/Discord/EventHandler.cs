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
    private readonly DiscordClient _client;

    public EventHandler(DiscordClient client)
    {
      this._client = client;

      this._client.MessageCreated += this.MessageCreated;
      this._client.ComponentInteractionCreated += this.OnInteraction;
    }

    private async Task<Task> MessageCreated(DiscordClient sender, MessageCreateEventArgs args)
    {
      if (new List<ulong> { Constants.Channels.AdBoardChannelId, Constants.Channels.TestingChannel }.Any(x => x == args.Channel.Id))
      {
        if ((args.Author.IsBot || this._client.CurrentApplication.Owners.Any(x => x.Id == args.Author.Id)) &&
            !args.Channel.Id.Equals(Constants.Channels.TestingChannel))
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