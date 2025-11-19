using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;

public class SendMessageScript : MonoBehaviour
{
    public GameObject lastMessage;
    Transform topScroll;
    [SerializeField] Transform scrollArea;
    [SerializeField] GameObject messagePrefab;
    [SerializeField] Transform content;
    [SerializeField] float xOffset;
    private void Start()
    {
        StartCoroutine(SendMessage());
    }
    IEnumerator SendMessage()
    {
        StartCoroutine(SendText("HAIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIIII", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("Bye", -1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("sus", -1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SendText("weeeeee", 1, null));
    }
    IEnumerator SendText(string text, int players, Image image)
    {
        GameObject newText = Instantiate(messagePrefab,content);
        newText.SetActive(true);
        MessageScript newTextScript = newText.GetComponentInChildren<MessageScript>();
        newTextScript.textField.text = text;
        newTextScript.textField.ForceMeshUpdate();
        Canvas.ForceUpdateCanvases();
        newTextScript.players = players;
        newTextScript.image = image;
        yield return null;
        newText.transform.position = FindNextPos(newTextScript);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(content.GetComponent<RectTransform>().sizeDelta.x, (content.InverseTransformPoint(topScroll.position).y - content.InverseTransformPoint(newTextScript.bottomPos.position).y));
        lastMessage = newText;
    }
    private void Update()
    {
        
    }
    string GetNextText()
    {
        return "Balls";
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
        return new Vector2(scrollArea.transform.position.x + thisMessage.players * xOffset,BottomPrevTextY() - ((thisMessage.players != lastMessage.GetComponentInChildren<MessageScript>().players) ? 0.3f:0.1f));
    }
    Vector2 FindFirstPos(MessageScript thisMessage)
    {
        topScroll = thisMessage.gameObject.transform.Find("topIndicator");
        return new Vector2(scrollArea.transform.position.x+thisMessage.players*xOffset,scrollArea.transform.position.y+2.5f);
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