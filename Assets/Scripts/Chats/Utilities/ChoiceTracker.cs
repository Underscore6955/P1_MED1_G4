using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System.Collections;

public class ChoiceTracker 
{
    ChatScript chat;
    public int curLine = 0;
    public List<GameObject> buttons = new List<GameObject>();
    public ChoiceTracker(ChatScript chat) { this.chat = chat; }
    public IEnumerator BuildChoice((List<string> choicesText,int choiceIndex) data)
    {
        while (!chat.open) yield return null;
        for (int i = 0; i < data.choicesText.Count; i++)
        {
            GameObject curObj = GameObject.Instantiate(ChatScript.choiceButtonPrefab,chat.choiceCanvas);
            curObj.SetActive(true);
            ChoiceButton curCB = curObj.GetComponent<ChoiceButton>();
            curCB.chat = chat;
            curCB.textElement.text = data.choicesText[i];
            curCB.choiceIndex = data.choiceIndex;
            curCB.choiceValue = i;
            curObj.transform.position = chat.choiceCanvas.transform.position-new Vector3(0,i);
            buttons.Add(curObj);
            yield return null;
            Vector2 curScale = MessageScript.FindSize(curCB.textElement);
            curCB.button.transform.localScale = curScale;
            curCB.bc.size = curScale;
        }
    }
    public int FindChoice(int choiceIndex, int choiceValue)
    {
        string[] lines = chat.choiceFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string lineText = lines[i];
            string numBuild = "";
            // ONLY WORKS IF CHOICE INDEX IS SORTED CORRECTLY 
            // Example: choiceIndex = 1, it checks index 2, then 4, then 7, then 9, then 10, checks the first digit of 10, which is 1
            // FIX LATER!!!!
            for (int j = 0; j < ("" + choiceIndex).Length; j++)
            {
                numBuild += lineText[j];
            }
            Int32.TryParse(numBuild, out int s);
            if (s != choiceIndex){ continue; }
            numBuild = "";
            int k = 0;
            for (int j = ("" + choiceIndex).Length + 1; j < lineText.Length; j++)
            {
                if (lineText[j] == '?') { if (k == choiceValue) { break; } k++; numBuild = ""; continue; }
                numBuild += lineText[j];
            }
            return Convert.ToInt32(numBuild)-1;
        }
        return 0;
    }
}
