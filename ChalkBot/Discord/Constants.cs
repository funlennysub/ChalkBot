using System.ComponentModel;
using DSharpPlus.SlashCommands;

namespace ChalkBot.Discord
{
  public static class Constants
  {
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

    public static class Guilds
    {
      public const ulong MelharucosDiscordId = 874214350469087264;
      public const ulong TestingDiscordId = 888022546103017502;
    }

    public static class Channels
    {
      public const ulong AdBoardChannelId = 878504975469273130;
      public const ulong ArtLobbyChannelId = 889276510714880052;
      public const ulong TestingChannel = 884126015956332564;
    }
  }
}