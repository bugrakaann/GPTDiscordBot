using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenAI.GPT3;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.Managers;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;

namespace GptQABotFinal.Services;

public class OpenAIChat
{
    private OpenAIService service;
    public OpenAIChat()
    {
        service = new OpenAIService(new OpenAiOptions()
        {
            ApiKey = "<apikey>"
        });
    }
    public async Task<string> GenerateResponse(string response, string model)
    {
        Console.WriteLine("Generating Response");
        var completionRequest = await service.ChatCompletion.CreateCompletion(new ChatCompletionCreateRequest
        {
            Messages = new List<ChatMessage>
            {
                ChatMessage.FromSystem("You are a helpful assistant."),
                ChatMessage.FromUser(response)
            },
            Model = model,
            MaxTokens = 200,
            
        });
        if (completionRequest.Successful)
        {
            return completionRequest.Choices.First().Message.Content;
        }
        else
        {
            return completionRequest.Error?.Message;
        }
    }
}