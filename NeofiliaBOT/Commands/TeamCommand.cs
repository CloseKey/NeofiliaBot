using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;

namespace Neofilia.BOT.Commands
{
    public class TeamCommand : BaseCommandModule
    {
        [Command("join")]
        public async Task Join(CommandContext ctx)
        {
            //TODO: ask in private and move user to corrisponding channel
            var joinEmbed = new DiscordEmbedBuilder()
            {
                Title = "React with the pub emoji!",
                Color = DiscordColor.Green,
                Description = "Test description",
                ImageUrl = ctx.Client.CurrentUser.AvatarUrl
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            //TODO: Could make this dynamic, for now thumbs up
            var exampleEmojiThumbsUp = DiscordEmoji.FromName(ctx.Client, ":+1");
            var exampleEmojiThumbsDown = DiscordEmoji.FromName(ctx.Client, ":-1");

            await joinMessage.CreateReactionAsync(exampleEmojiThumbsUp).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(exampleEmojiThumbsDown).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();

            var reactionResult = await interactivity.WaitForReactionAsync(x => x.User == ctx.User &&
                x.Message == joinMessage &&
                (x.Emoji == exampleEmojiThumbsUp || x.Emoji == exampleEmojiThumbsDown))
                .ConfigureAwait(false);

            if (reactionResult.Result.Emoji == exampleEmojiThumbsUp)
            {
                //var role = ctx.Guild.GetRole(sample);
                //await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }

            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }
    }
}
