using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Friend 
{
    public string Name;
    public int Id;
    public ChatScript chat;
    public Friend(int id, string name, ChatScript chatScript)
    {
        this.Id = id;
        this.Name = name;
        this.chat = chatScript;
    }
}
public class FriendList : MonoBehaviour
{
    public GameObject friendButtonPrefab;
    public Transform friendList;
    public ChatScript chatwithfriend; 
    private List<Friend> friends = new List<Friend>(); 

    public List<ChatScript> chats = new List<ChatScript>();

    void Start()
    {
        friends.Add(new Friend (1, "Sophie",chats[0]));
        friends.Add(new Friend (2, "Timmy",chats[1]));
        friends.Add(new Friend (3, "Peter",chats[2])); 



        foreach (Friend friend in friends)
        {
            Console.WriteLine(friend.Name);
        }
    }
}


