using Discord;
using Discord.Commands;
using GptQABotFinal.Services;
using OpenAI.GPT3.ObjectModels;

namespace GptQABotFinal.Modules;

public class General : ModuleBase<SocketCommandContext>
{
    [Command("gpt3")]
    [Summary("Chat with the bot")]
    public async Task Chat3Async([Remainder]string message)
    {
        OpenAIChat ai = new OpenAIChat();
        string response = await ai.GenerateResponse(message, Models.ChatGpt3_5Turbo);

        var embed = new EmbedBuilder()
            .WithTitle("Answer is below")
            .AddField("Answer", response)
            .AddField("Balance", "1000 tokens")
            .WithColor(new Color(0, 166, 126))
            .Build();
        
        await ReplyAsync(null,false,embed,null,null,new MessageReference(Context.Message.Id));
    }
    [Command("gpt4")]
    [Summary("Chat with the bot")]
    public async Task Chat4Async([Remainder]string message)
    {
        OpenAIChat ai = new OpenAIChat();
        string response = await ai.GenerateResponse(message, Models.Gpt_4);
        await ReplyAsync(response);
    }
    
}