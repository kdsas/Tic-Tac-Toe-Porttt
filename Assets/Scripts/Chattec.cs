using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Chattec : NetworkBehaviour
{

    //Chat History
    public List<string> chatHistory = new List<string>();
    public Text yij;
    //Keeps track of current message
    private string currentMessage;

    void SendMessage()
    {
        if (string.IsNullOrEmpty(currentMessage)) return;
        {
            CmdChatMessage(currentMessage);
            currentMessage = "";
            currentMessage = String.Empty;
        }
    }

    private void BottomChat()
    {
        currentMessage = GUI.TextField(new Rect(0, Screen.height - 20, 175, 20), currentMessage);
        if (GUI.Button(new Rect(200, Screen.height - 20, 75, 20), "Send"))
        {
            SendMessage();
        }
        GUILayout.Space(15);
        for (int i = chatHistory.Count - 1; i >= 0; i--)
         
          
        foreach (var msg in chatHistory)
        {
                yij.text = msg;
            }


    }

 


    //Chatbox
    private void OnGUI()
    {
        BottomChat();
        //TopChat();
    }

    [Command]
    void CmdChatMessage(string message)
    {
        RpcReceiveChatMessage(message);
        chatHistory.Add(message);
       
    }

    [ClientRpc]
    void RpcReceiveChatMessage(string message)
    {
        chatHistory.Add(message);
      
    }
}