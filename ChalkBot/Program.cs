using System;
using System.Threading.Tasks;
using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using EventHandler = ChalkBot.Discord.EventHandler;

namespace ChalkBot
{
  class Program
  {
    static void Main(string[] args)
    {
      Run().GetAwaiter().GetResult();
    }

    private static async Task Run()
    {
      var config = new Config();
      var discordConfig = new DiscordConfiguration
      {
        Token = config.Entries.Token,
        TokenType = TokenType.Bot,
        AutoReconnect = true,
        MinimumLogLevel = LogLevel.Information,
        Intents = DiscordIntents.Guilds | DiscordIntents.GuildMembers | DiscordIntents.GuildIntegrations |
                  DiscordIntents.GuildMessages | DiscordIntents.GuildPresences
      };

      var discordClient = new DiscordClient(discordConfig);

      discordClient.Ready += (_, _) =>
      {
        Console.WriteLine($"{discordClient.CurrentUser.Username} ready.");
        return Task.CompletedTask;
      };

      discordClient.ClientErrored += (_, args) =>
      {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
      };

      var serviceProvider = new ServiceCollection()
        .AddSingleton(discordClient)
        .AddSingleton<IConfig>(config)
        .BuildServiceProvider(true);

      serviceProvider.ResolveCommands();

      await discordClient.ConnectAsync();
      var _ = new EventHandler(discordClient);
      await Task.Delay(-1);
    }
  }
}