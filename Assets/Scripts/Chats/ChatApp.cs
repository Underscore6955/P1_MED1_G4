using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ChatApp : MonoBehaviour
{
    // sets how far text messages go out in a chat
    [SerializeField] float xOffset;
    [SerializeField] float centerXOffset;
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
    private void Start()
    {
        // go through each chat on the app, so each on in chatTexts list
        for (int i = 0; i<chatTexts.Count; i++)
        {
            // create a new chatscript on the gameobject
            ChatScript curChat = gameObject.AddComponent<ChatScript>();
            // assign aaaaallll the correct variables to the new chat
            curChat.enabled = false;
            curChat.textFile = chatTexts[i];
            curChat.choiceFile = choiceTexts[i];
            curChat.xOffset = this.xOffset;
            curChat.centerXOffset = this.centerXOffset;
            curChat.messagePrefab = this.messagePrefab;
            curChat.choiceButtonPrefab = this.choicePrefab;
            curChat.scroll = this.scroll;
            curChat.textBar = this.textBar;
            // make the things that need to be there, like content container and choice canvas, which holds the choice buttons
            curChat.content = new GameObject().transform;
            curChat.content.SetParent(transform);
            curChat.content.localScale = new Vector3(0.1f, 0.2f, 1f);
            curChat.content.gameObject.SetActive(true);
            curChat.content.gameObject.name = "content";
            curChat.choiceCanvas = new GameObject().transform;
            curChat.choiceCanvas.SetParent(transform);
            curChat.choiceCanvas.gameObject.SetActive(true);
            curChat.choiceCanvas.localPosition = Vector3.zero+Vector3.back *0.1f;
            curChat.choiceCanvas.localScale = new Vector3(0.1f, 0.2f, 1f);
            curChat.choiceCanvas.gameObject.name = "choiceCanvas";
            GetComponent<FriendList>().AddFriend(curChat,"Joe");
            // prepare some more things for the chat
            curChat.InitiateChat();
        }
    }
}
