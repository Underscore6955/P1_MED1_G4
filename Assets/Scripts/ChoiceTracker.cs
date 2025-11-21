using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ChoiceTracker : MonoBehaviour
{
    public int curLine = 0;
    [SerializeField] GameObject choiceButtonPrefab;
    public GetText GT;
    private void Start()
    {
        GT = GetComponent<GetText>();
    }
    public void BuildChoice((List<string> choicesText,int choiceIndex) data)
    {
        for (int i = 0; i < data.choicesText.Count; i++)
        {
            GameObject curObj = Instantiate(choiceButtonPrefab);
            ChoiceButton curCB = curObj.GetComponent<ChoiceButton>();
            curCB.CT = this;
            curCB.text = data.choicesText[i];
            curCB.choiceIndex = data.choiceIndex;
            curCB.choiceValue = i;
            curObj.transform.position = new Vector2(8.3f,2*i);
        }
    }
    public int FindChoice(int choiceIndex, int choiceValue)
    {
        string[] lines = GT.choiceFile.text.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string lineText = lines[i];
            string numBuild = "";
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
