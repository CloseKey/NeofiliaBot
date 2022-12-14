using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus;
using Microsoft.Extensions.Logging;
using Neofilia.BOT.Commands;
using Newtonsoft.Json;
using System.Text;
using DSharpPlus.Interactivity.Extensions;
using Neofilia.BOT.Helpers;

namespace Neofilia.BOT
{
    public class NeofiliaBot
    {
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public NeofiliaBot(IServiceProvider services)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("configJson.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = true,
                Services = services
            };
            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<AdminCommand>();
            Commands.RegisterCommands<BotCommand>();
            Commands.RegisterCommands<TeamCommand>();

            Client.ConnectAsync();
        }

        private Task OnClientReady(object o,ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
