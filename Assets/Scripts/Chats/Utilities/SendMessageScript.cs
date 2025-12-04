using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using System.IO;

public class SendMessageScript 
{
    ChatScript chat;
    public GameObject lastMessage;
    public SendMessageScript(ChatScript chat) { this.chat = chat; }
    public IEnumerator SendText((string text, int players, Texture2D image) data)
    {
        yield return chat.StartCoroutine(KeypressMessage(data.text));
        GameObject newText = Object.Instantiate(chat.messagePrefab, chat.content);
        newText.SetActive(true);
        MessageScript newTextScript = newText.GetComponentInChildren<MessageScript>();
        newTextScript.textField.text = data.text;
        newTextScript.players = data.players;
        newTextScript.image = data.image;
        newTextScript.CheckIfPlayers();
        yield return null;
        newTextScript.Sizing();
        newText.transform.position = FindNextPos(newTextScript);
        newText.transform.localPosition = new Vector3(newText.transform.localPosition.x, newText.transform.localPosition.y, 0);
        lastMessage = newText;
        if (newTextScript.image) { newTextScript.BuildImg(); newTextScript.FindImgPos(chat); }
        if (chat.open) chat.gameObject.GetComponent<Scrollable>().contentBottom = newTextScript.bottomPos;
        chat.bottomScroll = newTextScript.bottomPos;
    }
    Vector2 FindNextPos(MessageScript thisMessage)
    {
        if (lastMessage != null)
        {
            return FindNextNotFirst(thisMessage);
        }
        else 
        {
            return FindFirstPos(thisMessage); 
        }
    }
    Vector2 FindNextNotFirst(MessageScript thisMessage)
    {
        return new Vector2(FindNextX(thisMessage), 
            BottomPrevTextY() -
            0.5f*(thisMessage.topPos.position.y-thisMessage.bottomPos.position.y)
            -((thisMessage.players == 0 ? 0 :
            (thisMessage.players != lastMessage.GetComponentInChildren<MessageScript>().players) ? 0.3f:0.1f)));
    }
    public Vector2 FindNextNotFirst(float width, float center, int players, float height)
    {
        return new Vector2(FindNextX(width,center,players), BottomPrevTextY() - 0.5f * (height)-0.1f);
    }
    Vector2 FindFirstPos(MessageScript thisMessage)
    {
        chat.gameObject.GetComponent<Scrollable>().contentTop = thisMessage.gameObject.transform.Find("topIndicator");
        return new Vector2(FindNextX(thisMessage), chat.content.transform.position.y -0.5f * (thisMessage.topPos.position.y - thisMessage.bottomPos.position.y));
    }
    float FindNextX(MessageScript thisMessage)
    {
        return chat.content.transform.position.x + thisMessage.players*chat.xOffset- thisMessage.players*(thisMessage.width.position.x - thisMessage.bottomPos.position.x);
    }
    float FindNextX(float width, float center, int players)
    {
        return center + players * chat.xOffset - players * width*0.5f;
    }
    float BottomPrevTextY()
    {
        return lastMessage.GetComponentInChildren<MessageScript>().bottomPos.position.y;
    }
    IEnumerator KeypressMessage(string text)
    {
        string textBuild = "";
        for(int i = 0; i<text.Length; i++)
        {
            Debug.Log(textBuild);
            while (!Input.GetKeyDown(KeyCode.Space))
            {
                yield return null;
            }
            textBuild += text[i];
        }
        while (!Input.GetKeyDown(KeyCode.KeypadEnter)) yield return null;
    }
}
