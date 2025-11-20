using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetText : MonoBehaviour
{
    [SerializeField] TextAsset testFile;
    string[] lines;
    void Start()
    {
        lines = testFile.text.Split('\n');
        FindAction(0);
        FindAction(1);
    }
    public void FindAction(int line)
    {
        if (lines[line][0] == '"')
        {
            StartCoroutine(GetComponent<SendMessageScript>().SendText(BuildNextText(line)));
        }
        else
        {
            Tuple<List<string>,int> choice = BuildChoice(line);
            foreach(string s in choice.Item1)
            {
                Debug.Log(s);
            }
            Debug.Log(choice.Item2);
        }
    }
    (string, int, Image) BuildNextText(int line)
    {
        string lineText = lines[line];
        string textBuild = "";
        string imgNameBuild = null;
        bool img = false;
        for (int i = 2; i < lineText.Length; i++)
        {
            if (lineText[i] == '|') { img = true; }
            if (img) imgNameBuild += lineText[i];
            else textBuild += lineText[i];
        }
        return (textBuild, lineText[1] == '*' ? 1 : -1, null);
    }
    Tuple<List<string>, int> BuildChoice(int line)
    {
        List<string> listToReturn = new List<string>();
        string lineText = lines[line];
        string textBuild = "";
        int num = -1;
        for (int i = 1; i < lineText.Length; i++)
        {
            if (lineText[i] == '£') 
            {
                if (num < 0) num = Convert.ToInt32(textBuild); else listToReturn.Add(textBuild);
                    textBuild = "";
            }
            else textBuild += lineText[i];
        }
        return Tuple.Create(listToReturn,num);
    }
}