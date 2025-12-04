using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ChatApp : MonoBehaviour
{
    [SerializeField] float xOffset;
    [SerializeField] List<TextAsset> chatTexts = new List<TextAsset>();
    [SerializeField] List<TextAsset> choiceTexts = new List<TextAsset>();
    [SerializeField] List<OpenChatButton> openChatButtons = new List<OpenChatButton>();
    [SerializeField] GameObject messagePrefab;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] Scrollable scroll;
    [SerializeField] GameObject textBar;
    private void Awake()
    {
        for (int i = 0; i<chatTexts.Count; i++)
        {
            ChatScript curChat = gameObject.AddComponent<ChatScript>();
            curChat.enabled = false;
            curChat.textFile = chatTexts[i];
            curChat.choiceFile = choiceTexts[i];
            curChat.xOffset = this.xOffset;
            curChat.messagePrefab = this.messagePrefab;
            curChat.choiceButtonPrefab = this.choicePrefab;
            curChat.scroll = this.scroll;
            curChat.textBar = this.textBar;

            curChat.content = Instantiate(new GameObject(), transform).transform;
            curChat.content.localScale = new Vector3(0.1f, 0.2f, 1f);
            curChat.content.gameObject.SetActive(true);
            curChat.choiceCanvas = Instantiate(new GameObject(), transform).transform;
            curChat.choiceCanvas.gameObject.SetActive(true);
            curChat.choiceCanvas.localScale = new Vector3(0.1f, 0.2f, 1f);
            curChat.choiceCanvas.gameObject.name = "ChoiceCanvas";
            openChatButtons[i].chat = curChat;
            curChat.InitiateChat();
        }
    }
}
