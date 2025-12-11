using NUnit.Framework;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SendMessageScript 
{
    // same as choicetracker
    ChatScript chat;
    public GameObject lastMessage;
    public SendMessageScript(ChatScript chat) { this.chat = chat; }
    // this method is used whenever a message is sent 
    public IEnumerator SendText((string text, int players, Texture2D image) data)
    {
        // ask frederik :p good practice for everyone involved
        yield return new WaitForSeconds(GetText.CalcDelay(data.text.Length * -data.players));
        if (data.players == 1)
        {
            yield return chat.StartCoroutine(KeypressMessage(data.text));
            chat.AS.PlayOneShot((AudioClip)Resources.Load("Sound/MessageSent"));
        }
        else chat.AS.PlayOneShot(chat.open ? (AudioClip)Resources.Load("Sound/MessageSent") : (AudioClip)Resources.Load("Sound/MessageReceive"));
            // we create the new message and do some stuff to it, and get the message script from it
            GameObject newText = Object.Instantiate(chat.messagePrefab, chat.content);
        newText.name = "message";
        newText.SetActive(true);
        MessageScript newTextScript = newText.GetComponentInChildren<MessageScript>();
        // assign the values
        newTextScript.textField.text = data.text;
        newTextScript.players = data.players;
        newTextScript.image = data.image;
        // see message script
        newTextScript.CheckIfPlayers();
        // we need to wait a frame because fuckass unity is too slow to render text
        yield return null;
        // size the message correctly (see message script)
        newTextScript.Sizing();
        // finds the correct position
        (Vector2, bool) nextPos = FindNextPos(newTextScript);
        newText.transform.position = nextPos.Item1;
        if (nextPos.Item2) { (float,Transform) newTop = chat.scroll.SetTop(newTextScript.topPos, newTextScript.bottomPos,chat.open); chat.firstSize = newTop.Item1; chat.origin = newTop.Item2; chat.topScroll = newTextScript.topPos; }
        // sometimes the message would be placed in a weird way on the z axis, meaning kinda like behind the camera so it cannot be seen
        // not entirely sure why, but we just place it at 0
        newText.transform.localPosition = new Vector3(newText.transform.localPosition.x, newText.transform.localPosition.y, 0);
        lastMessage = newText;
        // if there is an image in the text, we create that (see image script)
        if (newTextScript.image) { newTextScript.BuildImg(); newTextScript.FindImgPos(chat); }
        // if the chat is open, we set the bottom of the scroll to this message, otherwise just remember it, so it does become the bottom when you do open the chat
        if (chat.open) chat.scroll.contentBottom = newTextScript.bottomPos;
        chat.bottomScroll = newTextScript.bottomPos;
        chat.scroll.ScrollToPos();
    }
    // checks if first or not first message
    (Vector2, bool) FindNextPos(MessageScript thisMessage)
    {
        if (lastMessage != null)
        {
            return (FindNextNotFirst(thisMessage),false);
        }
        else 
        {
            return (FindFirstPos(thisMessage),true); 
        }
    }
    // name explains it
    Vector2 FindNextNotFirst(MessageScript thisMessage)
    {
        // does some funny math stuff, but it basically places the message correctly, according to what side it should be on, the size of the text
        // cus it needs to be aligned with the side, and also below the previous message, with a distance depending on whether the last message was from the same person
        return new Vector2(FindNextX(thisMessage), 
            BottomPrevTextY() -
             0.5f *(thisMessage.topPos.position.y-thisMessage.bottomPos.position.y)
            -(chat.transform.localScale.x / 10f)*(((thisMessage.players != lastMessage.GetComponentInChildren<MessageScript>().players) ? 0.3f:0.1f)));
    }
    // same as the other one, this just uses something called overloading, this just makes it kinda easier maybe ask me for more if need be
    public Vector2 FindNextNotFirst(float width, float center, int players, float height)
    {
        return new Vector2(FindNextX(width,center,players), BottomPrevTextY() - (chat.transform.localScale.x / 10f)*0.5f * (height)-0.1f);
    }
    // very similar logic to FindNextNotFirst, just dosent use lastMessage, since that doesnt exist, obviously
    Vector2 FindFirstPos(MessageScript thisMessage)
    {
        // well this works the math is fine
        return new Vector2(FindNextX(thisMessage), chat.content.transform.position.y -0.5f * (thisMessage.topPos.position.y - thisMessage.bottomPos.position.y)-chat.transform.localScale.x/10*0.4f);
    }
    // finds the correct x value for where the next message needs to be, according to its size and sender
    float FindNextX(MessageScript thisMessage)
    {
        return chat.content.transform.position.x  + thisMessage.players*chat.xOffset * (chat.gameObject.transform.localScale.x / 10) - thisMessage.players*(thisMessage.width.position.x - thisMessage.bottomPos.position.x);
    }
    // overloading again, but same method
    float FindNextX(float width, float center, int players)
    {
        return center + players * chat.xOffset * (chat.gameObject.transform.localScale.x / 10) - players * width*0.5f;
    }
    // used to find the bottom of the last message
    float BottomPrevTextY()
    {
        return lastMessage.GetComponentInChildren<MessageScript>().bottomPos.position.y;
    }
    // once again ask frederik :3
    IEnumerator KeypressMessage(string text)
    {
        string textBuild = "";
        RawImage bar = chat.textBar.GetComponent<RawImage>();
        Coroutine flash = chat.StartCoroutine(BarFlash(bar));
        for(int i = 0; i<text.Length; i++)
        {
            while (!IsPressingLetter())
            {
                yield return null;
            }
            chat.AS.PlayOneShot((AudioClip)Resources.Load("Sound/click_" + Random.Range(1, 9)));
            yield return null;
            textBuild += text[i];
            chat.textBar.GetComponentInChildren<TMP_Text>().text = textBuild;
        }
        // return key is enter
        while (!Input.GetKeyDown(KeyCode.Return)) { yield return null; }
        chat.StopCoroutine(flash);
        bar.color = Color.white;
        // set the text bar text correctly
        chat.textBar.GetComponentInChildren<TMP_Text>().text = "";
    }
    // added this which just checks if any of the keys you pressed this frame are letters
    bool IsPressingLetter()
    {
        if (!chat.open) return false;
        foreach (char c in Input.inputString)
        {
            if (char.IsLetter(c) || char.IsNumber(c)) return true;
        }
        return false;
    }
    IEnumerator BarFlash(RawImage bar)
    {
        bool gray = false;
        while (true)
        {
            bar.color = gray ? new Color(0.75f,0.75f,0.75f) : Color.white;
            gray = !gray;
            yield return new WaitForSeconds(0.7f);
        }
    }
}
