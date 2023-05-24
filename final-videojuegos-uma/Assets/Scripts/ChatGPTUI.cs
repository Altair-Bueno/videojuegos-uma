using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatGPTUI : MonoBehaviour
{
    // Start is called before the first frame update
    public ChatGPT chat;
    public string prompt;

    void Start()
    {
    }


    private bool done;

    private void Update()
    {
        if (!done)
        {
            done = true;
            PrintOnConsole();
        }
    }

    public async void PrintOnConsole()
    {
        chat.AppendMessage(prompt);
        var response = await chat.GetResponse();
        print(response);
    }

}
