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
      public static readonly ulong MelharucosDiscord = 874214350469087264;
      public static readonly ulong TestingDiscord = 888022546103017502;
    }

    public static class Channels
    {
      public static readonly ulong AdBoardChannelId = 878504975469273130;
      public static readonly ulong ArtLobbyChannelId = 889276510714880052;
      public static readonly ulong TestingChannel = 884126015956332564;
    }
  }
}