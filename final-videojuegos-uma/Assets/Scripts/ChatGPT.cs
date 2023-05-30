using System;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using UnityEngine;
using YamlDotNet.Serialization;

public class ChatGPT : MonoBehaviour
{
    public TextAsset openaiKeyAsset;
    public TextAsset loreAsset;

    private OpenAIAPI api;
    private Conversation conversation;

    // Start is called before the first frame update
    private void Start()
    {
        ClearChat();
    }

    public void AppendMessage(string message)
    {
        conversation.AppendUserInput(message);
    }

    public void AppendSystemMessage(string message)
    {
        conversation.AppendSystemMessage(message);
    }

    public void ClearChat()
    {
        api = new OpenAIAPI(openaiKeyAsset.text);
        var deserializer = new DeserializerBuilder().Build();
        var lore = deserializer.Deserialize<ChatGPTStory>(loreAsset.text);
        var config = new ChatRequest();
        config.Model = Model.ChatGPTTurbo;
        conversation = api.Chat.CreateConversation(config);
        conversation.AppendSystemMessage(lore.main);
        foreach (var example in lore.examples)
        {
            conversation.AppendUserInput(example.userInput);
            conversation.AppendExampleChatbotOutput(example.response);
        }
    }

    public async Task<string> GetResponse()
    {
        var response = await conversation.GetResponseFromChatbotAsync();
        return response;
    }
}

[Serializable]
internal class ChatGPTStory
{
    public string main;
    public ChatGPTExample[] examples;
}

[Serializable]
internal class ChatGPTExample
{
    public string userInput;
    public string response;
}