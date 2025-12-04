using System.IO;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ChoiceButton : PressableObject
{
    public int choiceIndex {  private get; set; }
    public int choiceValue { private get; set; }
    public TMP_Text textElement;
    public string filePath;
    public ChatScript chat;
    public GameObject button;
    // if you press this it does what it needs to
    public override void Release()
    {
        StartCoroutine(SendMessage());
    }
    IEnumerator SendMessage()
    {
        // it finds which line it needs to go to, after this message has been sent
        chat.CT.curLine = chat.CT.FindChoice(choiceIndex, choiceValue);
        // we move the choices away, we cant destroy them yet, because this script is on one of them, and we cant kill this script quite yet
        foreach (GameObject g in chat.CT.buttons) { g.transform.position += Vector3.left * 999f; }
        // wait for the player to type the message
        yield return StartCoroutine(chat.SMS.SendText((textElement.text, 1, null)));
        // make it so that choice tracker can continue after message has been sent
        chat.choosing = false;
    }
    // ignore probably wont be used
    void FileUpdate()
    {
        File.WriteAllText(filePath,"");
    }
}
