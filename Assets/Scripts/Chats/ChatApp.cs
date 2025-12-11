using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ChatApp : MonoBehaviour
{
    // sets how far text messages go out in a chat
    [SerializeField] float xOffset;
    [SerializeField] float choiceOffset;
    // text files for chats
    //[SerializeField] List<TextAsset> chatTexts = new List<TextAsset>();
    //// choice files for chats
    //// these two are very important the corresponding files are in the same order in the lists
    //[SerializeField] List<TextAsset> choiceTexts = new List<TextAsset>();
    // prefabs
    [SerializeField] GameObject textBarPrefab;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject choicePrefab;
    // the correct scroll script and text bar
    [SerializeField] Scrollable scroll;

    [SerializeField] Transform chatContent;
    [SerializeField] Transform choiceLoc;
    [SerializeField] Transform textBarLoc;
    public static ChatScript activeChat;

    [SerializeField] ScaleScript SC;
    public static ChatApp chatInstance;
    static public int notifs;
    private void Awake()
    {
        chatInstance = this;
        transform.position += Vector3.right * 500;
        // go through each chat on the app, so each on in chatTexts list
        //for (int i = 0; i<chatTexts.Count; i++)
        //{
        //    AddChat(chatTexts[i], choiceTexts[i]);
        //}
    }
    public ChatScript AddChat(TextAsset textFile, TextAsset choiceFile)
    {
        // create a new chatscript on the gameobject
        ChatScript curChat = gameObject.AddComponent<ChatScript>();
        // assign aaaaallll the correct variables to the new chat
        curChat.enabled = false;
        curChat.textFile  = textFile;
        curChat.choiceFile = choiceFile;
        curChat.xOffset = this.xOffset;
        curChat.choiceOffset = this.choiceOffset;
        curChat.messagePrefab = this.messagePrefab;
        curChat.choiceButtonPrefab = this.choicePrefab;
        curChat.scroll = this.scroll;
        curChat.textBar = Instantiate(textBarPrefab,transform);
        curChat.textBar.transform.position = textBarLoc.position;
        curChat.textBar.SetActive(false);
        // make the things that need to be there, like content container and choice canvas, which holds the choice buttons
        curChat.content = new GameObject().transform;
        curChat.content.SetParent(chatContent);
        curChat.content.localScale = new Vector3(0.1f, 0.2f, 1f);
        curChat.content.localPosition = Vector3.zero;
        curChat.content.gameObject.SetActive(true);
        curChat.content.gameObject.name = "content";
        curChat.content.transform.SetAsFirstSibling();
        curChat.choiceCanvas = new GameObject().transform;
        curChat.choiceCanvas.SetParent(transform);
        curChat.choiceCanvas.gameObject.SetActive(true);
        curChat.choiceCanvas.localPosition = Vector3.zero + Vector3.back * 0.12f;
        curChat.choiceCanvas.localScale = new Vector3(0.1f, 0.2f, 1f);
        curChat.choiceCanvas.transform.position = choiceLoc.position;
        curChat.choiceCanvas.gameObject.name = "choiceCanvas";
        curChat.AS = gameObject.AddComponent<AudioSource>();
        // prepare some more things for the chat
        curChat.InitiateChat();
        curChat.GT.PrepText();
        GetComponent<FriendList>().AddFriend(curChat, curChat.friendName,curChat.pfp);
        if (curChat.friendName == "Jack")
        {
            curChat.SC = this.SC;
        }
        return curChat;
    }
    public static void ChangeChat(ChatScript chat)
    {
        if (activeChat && activeChat != chat) activeChat.Close();
        activeChat = chat;
    }
}
