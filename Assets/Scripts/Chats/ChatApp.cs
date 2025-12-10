using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class ChatApp : MonoBehaviour
{
    // sets how far text messages go out in a chat
    [SerializeField] float xOffset;
    [SerializeField] float choiceOffset;
    // text files for chats
    [SerializeField] List<TextAsset> chatTexts = new List<TextAsset>();
    // choice files for chats
    // these two are very important the corresponding files are in the same order in the lists
    [SerializeField] List<TextAsset> choiceTexts = new List<TextAsset>();
    // prefabs
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject choicePrefab;
    // the correct scroll script and text bar
    [SerializeField] Scrollable scroll;
    [SerializeField] GameObject textBar;

    [SerializeField] Transform chatContent;
    [SerializeField] Transform choiceLoc;
    static ChatScript activeChat;

    [SerializeField] ScaleScript SC;
    private void Start()
    {
        // go through each chat on the app, so each on in chatTexts list
        for (int i = 0; i<chatTexts.Count; i++)
        {
            AddChat(chatTexts[i], choiceTexts[i]);
        }
    }
    public void AddChat(TextAsset textFile, TextAsset choiceFile)
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
        curChat.textBar = this.textBar;
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
        curChat.choiceCanvas.localPosition = Vector3.zero + Vector3.back * 0.1f;
        curChat.choiceCanvas.localScale = new Vector3(0.1f, 0.2f, 1f);
        curChat.choiceCanvas.transform.position = choiceLoc.position;
        curChat.choiceCanvas.gameObject.name = "choiceCanvas";
        curChat.AS = gameObject.AddComponent<AudioSource>();
        // prepare some more things for the chat
        curChat.InitiateChat();
        curChat.GT.PrepText();
        GetComponent<FriendList>().AddFriend(curChat, curChat.friendName);
        if (curChat.friendName == "Phillip")
        {
            curChat.SC = this.SC;
            curChat.SC.chat = curChat;
        }
    }
    public static void ChangeChat(ChatScript chat)
    {
        if (activeChat && activeChat != chat) activeChat.Close();
        activeChat = chat;
    }
}
