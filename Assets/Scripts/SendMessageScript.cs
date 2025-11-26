using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using System.IO;
using System.Runtime.CompilerServices;

public class SendMessageScript : MonoBehaviour
{
    public GameObject lastMessage;
    Transform topScroll;
    [SerializeField] Transform scrollArea;
    [SerializeField] GameObject messagePrefab;
    public Transform content;
    [SerializeField] float xOffset;
    public IEnumerator SendText((string text, int players, Texture2D image) data)
    {
        GameObject newText = Instantiate(messagePrefab, content);
        newText.SetActive(true);
        MessageScript newTextScript = newText.GetComponentInChildren<MessageScript>();
        newTextScript.textField.text = data.text;
        newTextScript.players = data.players;
        newTextScript.image = data.image;
        newTextScript.CheckIfPlayers();
        yield return null;
        newTextScript.Sizing();
        newText.transform.position = FindNextPos(newTextScript);
        lastMessage = newText;
        if (newTextScript.image) { newTextScript.BuildImg(); newTextScript.findImgPos(this); }
        GetComponent<Scrollable>().contentBottom = newTextScript.bottomPos;
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
        topScroll = thisMessage.gameObject.transform.Find("topIndicator");
        GetComponent<Scrollable>().contentTop = topScroll;
        return new Vector2(FindNextX(thisMessage), content.transform.position.y -0.5f * (thisMessage.topPos.position.y - thisMessage.bottomPos.position.y));
    }
    float FindNextX(MessageScript thisMessage)
    {
        return content.transform.position.x + thisMessage.players*xOffset- thisMessage.players*(thisMessage.width.position.x - thisMessage.bottomPos.position.x);
    }
    float FindNextX(float width, float center, int players)
    {
        return center + players * xOffset - players * width*0.5f;
    }
    float BottomPrevTextY()
    {
        return lastMessage.GetComponentInChildren<MessageScript>().bottomPos.position.y;
    }
}
