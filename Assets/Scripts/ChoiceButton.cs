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
    public ChoiceTracker CT;
    public GameObject button;
    public override void Release()
    {
        StartCoroutine(SendMessage());
    }
    IEnumerator SendMessage()
    {
        CT.curLine = CT.FindChoice(choiceIndex, choiceValue);
        yield return StartCoroutine(CT.GT.SMS.SendText((textElement.text, 1, null)));
        CT.GT.choosing = false;
    }

    void FileUpdate()
    {
        File.WriteAllText(filePath,"");
    }
}
