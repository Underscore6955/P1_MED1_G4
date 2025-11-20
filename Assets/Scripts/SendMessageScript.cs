using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using System.IO;

public class SendMessageScript : MonoBehaviour
{
    public GameObject lastMessage;
    Transform topScroll;
    [SerializeField] Transform scrollArea;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] Transform content;
    [SerializeField] float xOffset;
    public IEnumerator SendText((string text, int players, Image image) data)
    {
        GameObject newText = Instantiate(messagePrefab,content);
        newText.SetActive(true);
        MessageScript newTextScript = newText.GetComponentInChildren<MessageScript>();
        newTextScript.textField.text = data.text;
        newTextScript.textField.ForceMeshUpdate();
        Canvas.ForceUpdateCanvases();
        newTextScript.players = data.players;
        newTextScript.image = data.image;
        yield return null;
        newText.transform.position = FindNextPos(newTextScript);
        GetComponent<Scrollable>().contentBottom = newTextScript.bottomPos;
        lastMessage = newText;
    }
    Vector2 FindNextPos(MessageScript thisMessage)
    {
        if (lastMessage != null) 
        {
            if (thisMessage.image) return FindNextImg(thisMessage);
            return FindNextText(thisMessage);
        } 
        else return FindFirstPos(thisMessage);
    }
    Vector2 FindNextText(MessageScript thisMessage)
    {
        return new Vector2(FindNextX(thisMessage), 
            BottomPrevTextY() -
            0.5f*(thisMessage.topPos.position.y-thisMessage.bottomPos.position.y)
            -((thisMessage.players != lastMessage.GetComponentInChildren<MessageScript>().players) ? 0.3f:0.1f));
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
    Vector2 FindNextImg(MessageScript thisMessage)
    {
        return Vector2.zero;
    }
    float BottomPrevTextY()
    {
        return lastMessage.GetComponentInChildren<MessageScript>().bottomPos.position.y;
    }
}
