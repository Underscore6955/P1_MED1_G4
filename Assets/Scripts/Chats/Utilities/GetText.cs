using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetText 
{
    ChatScript chat;
    string[] lines;
    public GetText(ChatScript chat) { this.chat = chat; }
    public void StartChat()
    {
        lines = chat.textFile.text.Split('\n');
        chat.StartCoroutine(FindAction(0));
    }
    public IEnumerator FindAction(int line)
    {
        if (lines[line][0] == '"')
        {
            chat.StartCoroutine(chat.SMS.SendText(BuildNextText(lines,line)));
            yield return new WaitForSeconds(1);
            chat.CT.curLine++;
        }
        else if (lines[line][0] == '~')
        {
            yield break;
        }
        else
        {
            chat.choosing = true;
            chat.StartCoroutine(chat.CT.BuildChoice(BuildChoice(line)));
            while (chat.choosing) yield return null;
            foreach (GameObject g in chat.CT.buttons) { GameObject.Destroy(g); }
            chat.CT.buttons.Clear();
            yield return new WaitForSeconds(1);
        }
        chat.StartCoroutine(FindAction(chat.CT.curLine));
    }
    public static (string, int, Texture2D) BuildNextText(string[] curLines,int line)
    {
        string lineText = curLines[line];
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
    public static Texture2D FindImg(string name)
    {
        return Resources.Load<Texture2D>("Social Media/profile pictures/" + name);
    }
}