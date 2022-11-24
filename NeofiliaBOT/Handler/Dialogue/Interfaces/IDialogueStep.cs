using DSharpPlus.Entities;
using DSharpPlus;

namespace Neofilia.BOT.Handler.Dialogue.Interfaces
{
    public interface IDialogueStep
    {
        Action<DiscordMessage> OnMessageAdded { get; set; }
        IDialogueStep NextStep { get; }
        Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user);
    }
}
