using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus;
using Neofilia.DAL;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.EntityFrameworkCore;
using Neofilia.DAL.Models;

namespace Neofilia.BOT.Services
{
    public class TriviaButtonService
    {
    //    private readonly Dictionary<ulong, State> _states = new();
    //    private readonly NeofiliaDbContext _context;

    //    public TriviaButtonService(DiscordClient client, NeofiliaDbContext context)
    //    {
    //        client.ComponentInteractionCreated += Handle;
    //        _context = context;
    //    }

    //    public async void AddTrivia(DiscordChannel channel, CommandContext ctx, TimeSpan wait, string questionType = null)
    //    {

    //        var questions = new List<Question>();
    //        if (questionType == null)
    //            questions = _context.Question.ToList();
    //        questions = _context.Question.Where(x => x.Category.Equals(questions)).ToList();
    //        if (questions.Any())
    //        { }  //TODO: it's empty!

    //        var question = questions.FirstOrDefault();//TODO: Built dynamic getting one at random

    //        var pollEmbed = new DiscordMessageBuilder
    //        {
    //            Content = question?.Description,
    //        };
    //        var button1 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "One", question?.Option1.ToString());
    //        var button2 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "Two", question?.Option2.ToString());
    //        var button3 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "Three", question?.Option3.ToString());
    //        var button4 = new DiscordButtonComponent(DSharpPlus.ButtonStyle.Primary, "Four", question?.Option4.ToString());
    //        var buttons = new DiscordComponent[] { button1, button2, button3, button4 };
    //        pollEmbed.AddComponents(buttons);
    //        var pollMessage = await pollEmbed.SendAsync(ctx.Channel);
    //    }

    //    //public async State? GetTriviaResults(DiscordChannel channel, CommandContext ctx, DiscordMessage discordMessage) {
    //    //    var interactivity = ctx.Client.GetInteractivity();
    //    //    var result = await interactivity.WaitForButtonAsync(discordMessage);
    //    //    return _states.Add(new KeyValuePair<ulong, State> { Value: 3,   });
    //    //}

    //    private async Task Handle(DiscordClient c, ComponentInteractionCreateEventArgs a)
    //    {
    //        // Check the ID, and insert into the state here
    //    }
    //}

    //public class State
    //{
    //    public DiscordUser discordUser { get; set; }
    //    public bool isAlive { get; set; }
    //    public int selected { get; set; }
    }
}
