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

            curChat.content = Instantiate(new GameObject(), transform).transform;
            curChat.choiceCanvas = Instantiate(new GameObject(), transform).transform;
            curChat.choiceCanvas.gameObject.AddComponent<Canvas>().renderMode = RenderMode.WorldSpace;
            openChatButtons[i].chat = curChat;
        }
    }
}
