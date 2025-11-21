using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;

public class ChoiceButton : PressableObject
{
    public int choiceIndex {  private get; set; }
    public int choiceValue { private get; set; }
    public string text;
    public string filePath;
    public ChoiceTracker CT;
    public override void Pressed()
    {
        StartCoroutine(SendMessage());
    }
    IEnumerator SendMessage()
    {
        CT.curLine = CT.FindChoice(choiceIndex, choiceValue);
        yield return StartCoroutine(CT.GT.SMS.SendText((text, 1, null)));
        CT.GT.choosing = false;
    }

    private void Update()
    {
        if (!CT.GT.choosing) { Destroy(gameObject); }
    }

    void FileUpdate()
    {
        File.WriteAllText(filePath,"");
    }
}
