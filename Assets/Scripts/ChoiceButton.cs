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
    public override void Release()
    {
        StartCoroutine(SendMessage());
    }
    IEnumerator SendMessage()
    {
        chat.CT.curLine = chat.CT.FindChoice(choiceIndex, choiceValue);
        yield return StartCoroutine(chat.SMS.SendText((textElement.text, 1, null)));
        chat.choosing = false;
    }

    void FileUpdate()
    {
        File.WriteAllText(filePath,"");
    }
}
