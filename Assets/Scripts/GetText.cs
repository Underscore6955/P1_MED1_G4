using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetText : MonoBehaviour
{
    [SerializeField] TextAsset textFile;
    public TextAsset choiceFile;
    [SerializeField] string dataFileName;
    string[] lines;
    ChoiceTracker CT;
    public SendMessageScript SMS;
    public bool choosing = false;
    void Start()
    {
        CT = GetComponent<ChoiceTracker>();
        SMS = GetComponent<SendMessageScript>();
        lines = textFile.text.Split('\n');
        StartCoroutine(FindAction(0));
    }
    public IEnumerator FindAction(int line)
    {
        if (lines[line][0] == '"')
        {
            StartCoroutine(GetComponent<SendMessageScript>().SendText(BuildNextText(line)));
            yield return new WaitForSeconds(1);
            CT.curLine++;
        }
        else if (lines[line][0] == '~')
        {
            yield break;
        }
        else
        {
            choosing = true;
            StartCoroutine(CT.BuildChoice(BuildChoice(line)));
            while (choosing) yield return null;
            foreach (GameObject g in CT.buttons) { Destroy(g); }
            CT.buttons.Clear();
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(FindAction(CT.curLine));
    }
    (string, int, Texture2D) BuildNextText(int line)
    {
        string lineText = lines[line];
        string textBuild = "";
        string imgNameBuild = null;
        bool img = false;
        for (int i = 2; i < lineText.Length-1; i++)
        {
            if (lineText[i] == '|') { img = true; continue; }
            if (img) imgNameBuild += lineText[i];
            else textBuild += lineText[i];
        }
        return (textBuild, lineText[1] == '*' ? 1 : -1, FindImg(imgNameBuild));
    }
    (List<string>, int) BuildChoice(int line)
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
        return (listToReturn,num);
    }
    Texture2D FindImg(string name)
    {
        return Resources.Load<Texture2D>("Test/" + name);
    }
}