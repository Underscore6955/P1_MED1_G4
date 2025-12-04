using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetText 
{
    // same as choicetracker
    ChatScript chat;
    string[] lines;
    public GetText(ChatScript chat) { this.chat = chat; }
    // gets called when the chat gets opened for the first time
    public void StartChat()
    {
        // split the textfile into lines, like choicetracker
        lines = chat.textFile.text.Split('\n');
        // we start the program with the first line in the story file
        chat.StartCoroutine(FindAction(0));
    }
    // used to decide what should happen, based on the line
    public IEnumerator FindAction(int line)
    {
        // if first character is " then it is a text message
        if (lines[line][0] == '"')
        {
            // wait for this message to be sent (will either be just 1 frame, if it is the other guy texting, otherwise it is until the player has finished typing)
            yield return chat.StartCoroutine(chat.SMS.SendText(BuildNextText(lines,line)));
            // wait 1 second D:
            yield return new WaitForSeconds(1);
            // go to the next line
            chat.CT.curLine++;
        }
        // if first message is ~ then cancel the code for now, doesnt do anything yet might never
        else if (lines[line][0] == '~')
        {
            yield break;
        }
        // finally if it is neither (means it should be -) it mean there is a choice
        else
        {
            // make sure the thing keeps going until a choice is made (see ChoiceButton)
            chat.choosing = true;
            // builds the buttons
            chat.StartCoroutine(chat.CT.BuildChoice(BuildChoice(line)));
            // continue when choice is made
            while (chat.choosing) yield return null;
            // kill the buttons when not needed
            foreach (GameObject g in chat.CT.buttons) { GameObject.Destroy(g); }
            chat.CT.buttons.Clear();
            // youll never guess it...
            yield return new WaitForSeconds(1);
        }
        // curline has now either been increased by 1, if normal text message, or set to another number if choice, so now do whatever it needs to do
        chat.StartCoroutine(FindAction(chat.CT.curLine));
    }
    // method to decode text messages
    // returns a string (text in the message, int which is 1 for players and -1 for other guy, and texture2d which can be null if no image)
    public static (string, int, Texture2D) BuildNextText(string[] curLines,int line)
    {
        // we need these variables
        string lineText = curLines[line];
        string textBuild = "";
        string imgNameBuild = null;
        bool img = false;
        // we are not interested in the first to characters right now, as they are " and * or . so we start at the third one, and then go through the rest
        for (int i = 2; i < lineText.Length-1; i++)
        {
            // if a | is found, start figuring out the name of the image instead of the text
            if (lineText[i] == '|') { img = true; continue; }
            if (img) imgNameBuild += lineText[i];
            else textBuild += lineText[i];
        }
        // returns the text, 1 if there is a * and -1 if there is a ., and an image, or maybe null, depending on the line
        return (textBuild, lineText[1] == '*' ? 1 : -1, FindImg("test/" + imgNameBuild));
    }
    // Method to decode what choices should be available
    // Returns a list of choices and an int, which is the choice index, used to see which choice should be used from choice file later (see BuildChoice in choicetracker)
    (List<string>, int) BuildChoice(int line)
    {
        // yk the drill
        List<string> listToReturn = new List<string>();
        string lineText = lines[line];
        string textBuild = "";
        int num = -1;
        // go through each letter in the line other than the first one
        for (int i = 1; i < lineText.Length; i++)
        {
            // go until it finds a £
            if (lineText[i] == '£') 
            {
                // if the choice index has not been found yet, meaning it is less than 0, we know that what we have MUST be a number and the choice index
                // if choice index has been found we add what we found to the choices, since it must be text
                // this is due to how we have written the files 
                if (num < 0) num = Convert.ToInt32(textBuild); else listToReturn.Add(textBuild);
                    textBuild = "";
            }
            // if not £ then add the character to textbuild
            else textBuild += lineText[i];
        }
        // we return the list of choices and choice index
        return (listToReturn,num);
    }
    // method used to find image from assets directly not that interesting
    public static Texture2D FindImg(string name)
    {
        return Resources.Load<Texture2D>(name);
    }
}