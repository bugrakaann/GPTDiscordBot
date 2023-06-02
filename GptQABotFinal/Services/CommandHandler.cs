using System;
using System.Collections.Generic;
using System.Reflection;
using Discord;
using Discord.Addons.Hosting;
using Discord.WebSocket;
using Microsoft.Extensions.Logging;
using Discord.Commands;
using Microsoft.Extensions.Configuration;


namespace GptQABotFinal.Services;

public class CommandHandler : DiscordClientService
{
    private readonly IServiceProvider _provider;
    private readonly CommandService _commandService;
    private readonly IConfiguration _config;
    private readonly DiscordSocketClient _client;


    public CommandHandler(DiscordSocketClient client, ILogger<CommandHandler> logger,  IServiceProvider provider, CommandService commandService, IConfiguration config) : base(client, logger)
    {
        _provider = provider;
        _commandService = commandService;
        _config = config;
        _client = client;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Client.MessageReceived += OnMessageReceived;

        await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
    }

    private async Task OnMessageReceived(SocketMessage socketMessage)
    {
        if (socketMessage is not SocketUserMessage message) return;
        if (message.Source != MessageSource.User) 
            return;

        int argPos = 0;
        if (!message.HasStringPrefix(_config["Prefix"], ref argPos) ) 
            return;
        // argPos = 0;
        // if (!message.HasMentionPrefix(Client.CurrentUser, ref argPos))
        //     return;

        var context = new SocketCommandContext(Client, message);
        await _commandService.ExecuteAsync(context, argPos, _provider);
    }
}