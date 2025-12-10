using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using System.Collections;

public class ChoiceTracker 
{
    // "Parent" class, used to contain all other chat things
    ChatScript chat;
    // Variable to keep track of where in the story you are
    public int curLine = 0;
    // List to remember which buttons to delete in choices
    public List<GameObject> buttons = new List<GameObject>();
    // constructor for the class
    public ChoiceTracker(ChatScript chat) { this.chat = chat; }

    // This method gets run whenever the story text document thingy says so (see GetText)
    public IEnumerator BuildChoice((List<string> choicesText,int choiceIndex) data)
    {
        // if the chat is not open, it should wait with building the choices until it is open
        while (!chat.open) yield return null;
        // go through each choice
        for (int i = 0; i < data.choicesText.Count; i++)
        {
            // Create the choice button obj and add the required stuff to it
            GameObject curObj = GameObject.Instantiate(chat.choiceButtonPrefab,chat.choiceCanvas);
            curObj.gameObject.name = "choiceButton";
            curObj.SetActive(true);
            ChoiceButton curCB = curObj.GetComponent<ChoiceButton>();
            curCB.chat = chat;
            curCB.textElement.text = data.choicesText[i];
            curCB.choiceIndex = data.choiceIndex;
            curCB.choiceValue = i;
            // place it correctly and scale it according to the size of the text
            curObj.transform.position = chat.choiceCanvas.transform.position-new Vector3(0,i*chat.choiceOffset * (chat.gameObject.transform.localScale.x / 10));
            buttons.Add(curObj);
            yield return null;
            Vector2 curScale = MessageScript.FindSize(curCB.textElement);
            curCB.button.transform.localScale = curScale;
            curCB.bc.size = curScale;
        }
    }
    // Method to decode what to do when a choice is made from choice file
    public int FindChoice(int choiceIndex, int choiceValue)
    {
        // Split text file into lines because \n is line break symbol
        string[] lines = chat.choiceFile.text.Split('\n');
        // go through each line 
        for (int i = 0; i < lines.Length; i++)
        {
            // some variables we need
            string lineText = lines[i];
            string numBuild = "";
            // ONLY WORKS IF CHOICE INDEX IS SORTED CORRECTLY 
            // Example: choiceIndex = 1, it checks index 2, then 4, then 7, then 9, then 10, checks the first digit of 10, which is 1
            // FIX LATER!!!!
            // checks the first couple of letters of the line, to see if it is the correct "choice id" or whatever
            // ("" + index).Length means if choiceindex has 3 digits, like 103, check the first 3 letters and check only the first for 6 
            for (int j = 0; j < ("" + choiceIndex).Length; j++)
            {
                numBuild += lineText[j];
            }
            // it gets it as a string, so we turn it into an int
            Int32.TryParse(numBuild, out int s);
            // see if it is the desired index, if not try the next line, continue means skip the rest of this iteration of the for loop, and just add 1 to i
            if (s != choiceIndex){ continue; }
            numBuild = "";
            int k = 0;
            // goes through [choice value] amount of ?s until it finds the value of what it needs to set curline to
            for (int j = ("" + choiceIndex).Length + 1; j < lineText.Length; j++)
            {
                if (lineText[j] == '?') { if (k == choiceValue) { break; } k++; numBuild = ""; continue; }
                numBuild += lineText[j];
            }
            return Convert.ToInt32(numBuild)-1;
        }
        // this SHOULD never happen, would only ever happen if somehow you entered a fully empty line i think, which wont happen
        // it has to be here tho, otherwise it will say "Not all paths return a value"
        return 0;
    }
}
