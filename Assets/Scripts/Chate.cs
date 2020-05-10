using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using System;
using UnityEngine.Networking.NetworkSystem;
public class Chate : NetworkBehaviour
{
    public string currentMessage;
    public List<string> chat;
    public Text chatee;
    public class MyMessages
    {

        public enum MyMessageTypes
        {
            CHAT_MESSAGE = 1000
              
    }

        public class ChatMessage : MessageBase
        {
            public string message;
         

        }

    }

  
    public void OnStartClient(NetworkClient mClient)
    {
        base.OnStartClient(); // base implementation is currently empty

        mClient.RegisterHandler((short)MyMessages.MyMessageTypes.CHAT_MESSAGE, OnClientChatMessage);
    }




    // hook into NetManagers server setup process
    public override void OnStartServer()
    {
        base.OnStartServer(); //base is empty
        NetworkServer.RegisterHandler((short)MyMessages.MyMessageTypes.CHAT_MESSAGE, OnServerChatMessage);
    }

    public void OnGUI()
    {
        GUILayout.BeginHorizontal(GUILayout.Width(250));

        currentMessage = GUILayout.TextField(currentMessage);
        foreach (var msg in chat)
        {
            GUILayout.Label(msg);
        }
        if (GUILayout.Button("Send"))
        {
            if (!string.IsNullOrEmpty(currentMessage))
            {
                MyMessages.ChatMessage msg = new MyMessages.ChatMessage();
                msg.message = currentMessage;
                NetworkManager.singleton.client.Send((short)MyMessages.MyMessageTypes.CHAT_MESSAGE, msg);
          
                currentMessage = String.Empty;
            }
        }

        GUILayout.EndHorizontal();

    }

    private void OnServerChatMessage(NetworkMessage netMsg)
    {
        var msg = netMsg.ReadMessage<StringMessage>();
        chatee.text=   msg.value;

        MyMessages.ChatMessage chat = new MyMessages.ChatMessage();
        chat.message = msg.value;

        if (msg.value == "This game is bad")
        {
            return;
        }
        else
        {
            NetworkServer.SendToAll((short)MyMessages.MyMessageTypes.CHAT_MESSAGE, chat);
        }
    }

    private void OnClientChatMessage(NetworkMessage netMsg)
    {
        MyMessages.ChatMessage chat = new MyMessages.ChatMessage();
        var msg = netMsg.ReadMessage<StringMessage>();
        chatee.text =  msg.value;

       
    }

}