using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using UnityEngine;
using UnityEngine.Serialization;

public class ChatGPT : MonoBehaviour
{
    public TextAsset openaiKeyAsset;
    public TextAsset loreAsset;

    private OpenAIAPI api;
    private Conversation conversation;

    // Start is called before the first frame update
    void Start()
    {
        api = new OpenAIAPI(openaiKeyAsset.text);
        var lore = JsonUtility.FromJson<ChatGPTStory>(loreAsset.text);
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

    public void AppendMessage(string message)
    {
        conversation.AppendUserInput(message);
    }

    public async Task<string> GetResponse()
    {
        var response = await conversation.GetResponseFromChatbotAsync();
        return response;
    }
}

[Serializable]
class ChatGPTStory
{
    public string main;
    public ChatGPTExample[] examples;
}

[Serializable]
class ChatGPTExample
{
    public string userInput;
    public string response;
}