using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class ScaleScript : MonoBehaviour
{
    [SerializeField] TextAsset questions;
    [SerializeField] GameObject questionPrefab;
    public ChatScript chat;
    string[] lines;
    int qNumber = 0;
    public static int disc;
    public static int stand;
    public bool choosing;
    public List<GameObject> buttons = new List<GameObject>();
    private void Start()
    {
        lines = questions.text.Split("\n");
    }
    (string, bool) ReadQuestion()
    {
        string qBuild = "";
        for (int i = 1; i < lines[qNumber].Length; i++)
        {
            qBuild += lines[qNumber][i];
        }
        return (qBuild, lines[qNumber][0] == '!');
    }
    IEnumerator BuildChoice()
    {
        (string, bool) curQ = ReadQuestion();
        yield return StartCoroutine(chat.SMS.SendText((curQ.Item1, -1, null)));
        for (int i = 1; i <= 7; i++)
        {
            ScaleButton curButton = Instantiate(questionPrefab, chat.choiceCanvas).GetComponent<ScaleButton>();
            buttons.Add(curButton.gameObject);
            curButton.transform.localPosition = new Vector3(i-4, 0, 0);
            curButton.SC = this;
            curButton.text.text = i.ToString();
            curButton.value = i;
            curButton.disc = curQ.Item2;
        }
        
    }
    public IEnumerator SendMessages(int questions)
    {
        int endQ = qNumber + questions;
        for (int i = qNumber; i < endQ; i++)
        {
            if (qNumber >= lines.Length) { yield break; }
            choosing = true;
            StartCoroutine(BuildChoice());
            while (choosing) yield return null;
            qNumber++;
            foreach (GameObject button in buttons) { Destroy(button);  }
            buttons.Clear();
        }
    }
    public IEnumerator StartTest()
    {
        for (int i = 0; i < chat.GT.lines.Length - 2; i++)
        {
            yield return StartCoroutine(chat.SMS.SendText(GetText.BuildNextText(chat.GT.lines,i)));
        }
    }
    public IEnumerator EndTest()
    {
        yield return StartCoroutine(SendMessages(20));
        for (int i = 0; i < chat.GT.lines.Length; i++)
        {
            yield return StartCoroutine(chat.SMS.SendText(GetText.BuildNextText(chat.choiceFile.text.Split('\n'), i)));
        }
        yield return null;
    }
}
