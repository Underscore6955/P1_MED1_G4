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
    public int disc;
    public int stand;
    public bool choosing;
    List<GameObject> buttons = new List<GameObject>();
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
        return (qBuild, lines[qNumber][0] == '*');
    }
    IEnumerator BuildChoice()
    {
        (string, bool) curQ = ReadQuestion();
        Debug.Log("AA");
        yield return StartCoroutine(chat.SMS.SendText((curQ.Item1, -1, null)));
        for (int i = 1; i <= 7; i++)
        {
            Debug.Log(curQ.Item1);
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
        for (int i = 0; i < questions; i++)
        {
            qNumber++;
            choosing = true;
            StartCoroutine(BuildChoice());
            while (choosing) yield return null;
            foreach (GameObject button in buttons) { Destroy(button);  }
            buttons.Clear();
        }
    }
}
