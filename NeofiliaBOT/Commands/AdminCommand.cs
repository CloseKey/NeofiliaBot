using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using NeofiliaBot.Handler.Dialogue.Steps;
using Neofilia.BOT.Handler.Dialogue.Steps;
using Neofilia.BOT.Handler.Dialogue;
using Neofilia.DAL;
using Neofilia.BOT.Helpers;
using DSharpPlus;

namespace Neofilia.BOT.Commands
{
    public class AdminCommand : BaseCommandModule
    {
        private readonly NeofiliaDbContext _context;

        public AdminCommand(NeofiliaDbContext context)
        {
            _context = context;
        }
        [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Pong").ConfigureAwait(false);
        }

        [Command("add")]
        public async Task UpdateTable(CommandContext ctx,
            [Description("Table to be updated")] string tableUpdate)
        {
            var csvHandler = new CsvHandler(_context);
            if (!csvHandler.BuildEntities(tableUpdate))
                await ctx.Channel.SendMessageAsync("Error").ConfigureAwait(false);
            else
                await ctx.Channel.SendMessageAsync("Done").ConfigureAwait(false);
        }
        [Command("delete")]
        public async Task DeleteTable(CommandContext ctx)
        {
            var questions = _context.Question.ToList();
            _context.RemoveRange(questions);
            _context.SaveChanges();
            await ctx.Channel.SendMessageAsync("Done").ConfigureAwait(false);
        }

        [Command("get")]
        public async Task GetQuestion(CommandContext ctx, [Description("Question Description to be searched for")] string questionDescription)
        {
            var questions = _context.Question.Where(x => x.Description.Contains(questionDescription)).ToList();
            if (questions.Count == 0)
                questions = _context.Question.ToList();
            foreach (var question in questions)
            {
                await ctx.Channel.SendMessageAsync(
                    $"Id: {question.Id} " +
                    $"\nTitolo: {question.Description} " +
                    $"\nDomanda Uno: {question.Option1} " +
                    $"\nDomanda Due {question.Option2} " +
                    $"\nDomanda Tre: {question.Option3} " +
                    $"\nDomanda Quattro: {question.Option4}").ConfigureAwait(false);
            }
        }

        [Command("question")]
        public async Task Question(CommandContext ctx)
        {
            //var interactivity = ctx.Client.GetInteractivity();

            //var question = await _context.Question.FirstOrDefaultAsync(x => x.Id.Equals(2)).ConfigureAwait(false);
            ////Built dynamic
            //var pollEmbed = new DiscordMessageBuilder
            //{
            //    Content = question?.Description,
            //};
            //var button1 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "One", question?.Option1.ToString());
            //var button2 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "Two", question?.Option2.ToString());
            //var button3 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "Three", question?.Option3.ToString());
            //var button4 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "Four", question?.Option4.ToString());
            //var buttons = new DiscordComponent[] { button1, button2, button3, button4 };
            //pollEmbed.AddComponents(buttons);
            //var pollMessage = await pollEmbed.SendAsync(ctx.Channel);
            //InteractivityResult<ComponentInteractionCreateEventArgs> a;
            //var result = await interactivity.WaitForButtonAsync(pollMessage);

            //await ctx.Channel.SendMessageAsync(string.Join("\n", result)).ConfigureAwait(false);
        }

        [Command("NeofiliaStarter")]
        public async Task ReactionRole(CommandContext ctx)
        {

            var confirmButton = new DiscordButtonComponent(ButtonStyle.Success, "StartNeofilia", "Confirm");

            var selectionBuilder = new DiscordMessageBuilder().WithContent("Welcome To Neofilia, Are You Ready To Test Yourself?").AddComponents(confirmButton);

            Task collectorTask(DiscordClient sender, DSharpPlus.EventArgs.ComponentInteractionCreateEventArgs e)
            {
                _ = Task.Run(async () =>
            {
                if (e.Id == "StartNeofilia")
                {
                    var inputStep = new TextStep("Enter something interesting!", null, 10);
                    var funnyStep = new IntStep("Haha, funny", null, maxValue: 100);

                    string input = string.Empty;
                    int value = 0;

                    inputStep.OnValidResult += (result) =>
                    {
                        input = result;

                        if (result == "something interesting")
                        {
                            inputStep.SetNextStep(funnyStep);
                        }
                    };

                    funnyStep.OnValidResult += (result) => value = result;

                    var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);

                    var inputDialogueHandler = new DialogueHandler(
                        ctx.Client,
                        userChannel,
                        ctx.User,
                        inputStep
                    );

                    bool succeeded = await inputDialogueHandler.ProcessDialogue().ConfigureAwait(false);

                    if (!succeeded) { return; }

                    await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);

                    await ctx.Channel.SendMessageAsync(value.ToString()).ConfigureAwait(false);

                }
            });
                return Task.CompletedTask;
            };

            await selectionBuilder.SendAsync(ctx.Channel);

            ctx.Client.ComponentInteractionCreated += collectorTask;
            await Task.Delay(200000000);
        }
    }
}
