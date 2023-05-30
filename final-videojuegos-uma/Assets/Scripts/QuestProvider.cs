using System.Collections;
using UnityEngine;

public class QuestProvider : MonoBehaviour
{
    public string objetive;
    public string[] objetives = { };
    public TextController textController;
    public ChatGPT chatGpt;

    // Start is called before the first frame update
    private void Start()
    {
        objetive = objetives[Random.Range(0, objetives.Length)];
        chatGpt.AppendSystemMessage($"The object you are looking for is: {objetive}");
    }

    public IEnumerator StartQuest(GirlCharacterMain girlCharacter)
    {
        // 1. Get girl's message
        var task = girlCharacter.chatGpt.GetResponse();
        yield return new WaitUntil(() => task.IsCompleted);
        var text = task.Result;
        yield return girlCharacter.textController.UpdateText(text);

        // 2. Pass the message to Gramma
        chatGpt.AppendMessage(text);
        task = chatGpt.GetResponse();
        yield return new WaitUntil(() => task.IsCompleted);
        text = task.Result;
        yield return textController.UpdateText(text);

        // 3. Pass the message to girl
        girlCharacter.chatGpt.AppendMessage(text);
        task = girlCharacter.chatGpt.GetResponse();
        yield return new WaitUntil(() => task.IsCompleted);
        text = task.Result;
        yield return girlCharacter.textController.UpdateText(text);
    }

    public IEnumerator EndQuest(GirlCharacterMain girlCharacter)
    {
        // 1. Get girl's message
        girlCharacter.chatGpt.AppendSystemMessage("You have found the requested item. Inform the old lady");
        var task = girlCharacter.chatGpt.GetResponse();
        yield return new WaitUntil(() => task.IsCompleted);
        var text = task.Result;
        yield return girlCharacter.textController.UpdateText(text);

        // 2. Pass the message to Gramma
        chatGpt.AppendMessage(text);
        task = chatGpt.GetResponse();
        yield return new WaitUntil(() => task.IsCompleted);
        text = task.Result;
        yield return textController.UpdateText(text);

        // 3. Pass the message to girl
        girlCharacter.chatGpt.AppendMessage(text);
        task = girlCharacter.chatGpt.GetResponse();
        yield return new WaitUntil(() => task.IsCompleted);
        text = task.Result;
        yield return girlCharacter.textController.UpdateText(text);
    }
}