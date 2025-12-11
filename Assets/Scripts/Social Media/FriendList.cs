using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class FriendList : MonoBehaviour
{
    [SerializeField] float buttonSpacing; // distance between buttons
    public GameObject friendButtonPrefab; //hver ven får sin egen knap
    public Transform friendList; //vores UI der indeholder venneknapperne
    private List<Friend> friends = new List<Friend>(); //Liste af venner
    [SerializeField] Scrollable scroll;

    public List<ChatScript> chats = new List<ChatScript>(); //Liste af alle chats
    void Start()
    {
        //friends.Add(new Friend("Sophie", chats[0]));
        //friends.Add(new Friend("Timmy", chats[1]));
        //friends.Add(new Friend("Peter", chats[2]));
        ////Når spillet starter er ovenstående registreret i spillet

        //foreach (Friend friend in friends)
        //{
        //    Console.WriteLine(friend.name);
        //}
        ////Friend(datatype), friend(variabelnavn), friends(liste med alle venner)
        ////gennemløber alle venner i listen, en ad gangen 
    }
    // method to add a new friend
    public void AddFriend(ChatScript chat, string name)
    {
        // adds friend to list of friends
        friends.Add(new Friend(name, chat));
        // create the button
        OpenChatButton curButton = Instantiate(friendButtonPrefab, friendList).GetComponent<OpenChatButton>();
        curButton.gameObject.SetActive(true);
        curButton.gameObject.name = friends.Count.ToString();
        // assign the correct values to the button
        curButton.friendName.text = name;
        curButton.chat = chat;
        chat.newNotif = curButton.newNotif;
        Transform curTrans = curButton.gameObject.transform;
        // place the button according to how many other buttons there 
        curTrans.position = friendList.position + Vector3.down * buttonSpacing * (friends.Count - 1) + Vector3.back*0.1f;
        curTrans.localScale = OSMechanics.ResizeImageToSize((Texture2D)curTrans.gameObject.GetComponent<RawImage>().texture, curTrans.gameObject.GetComponent<RectTransform>(),2);
        // find the bottom of the button for the scrolling
        scroll.contentBottom = curTrans.Find("bottom");
        // if it is the first time a friend is being added (no other friends exist) add some things to scroll
        if (friends.Count == 1) 
        {
            scroll.SetTop(curTrans.Find("top"), curTrans.Find("bottom"),true);
        }
        scroll.curScroll = scroll.MaxScroll();
        scroll.ScrollToPos();
    }
}
public class Friend 
{
    public string name; //navn på ven
    public ChatScript chat; //reference til chatscript
    public Friend( string name, ChatScript chatScript)
    {
        this.name = name;
        this.chat = chatScript;
    }
}

