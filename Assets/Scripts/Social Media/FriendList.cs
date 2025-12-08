using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class Friend 
{
    public string Name; //navn på ven
    public int Id; //tal der identificerer vennen
    public ChatScript chat; //reference til chatscript
    public Friend(int id, string name, ChatScript chatScript)
    {
        this.Id = id;
        this.Name = name;
        this.chat = chatScript;
    }
}
public class FriendList : MonoBehaviour
{
    public GameObject friendButtonPrefab; //hver ven får sin egen knap
    public Transform friendList; //vores UI der indeholder venneknapperne
    public ChatScript chatwithfriend; //Aktive chat med valgte ven
    private List<Friend> friends = new List<Friend>(); //Liste af venner

    public List<ChatScript> chats = new List<ChatScript>(); //Liste af alle chats
    void Start()
    {
        friends.Add(new Friend (1, "Sophie",chats[0]));
        friends.Add(new Friend (2, "Timmy",chats[1]));
        friends.Add(new Friend (3, "Peter",chats[2])); 
        //Når spillet starter er ovenstående registreret i spillet

        foreach (Friend friend in friends)
        {
            Console.WriteLine(friend.Name);
        }
        //Friend(datatype), friend(variabelnavn), friends(liste med alle venner)
        //gennemløber alle venner i listen, en ad gangen 
    }
}


