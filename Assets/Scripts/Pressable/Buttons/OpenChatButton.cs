using UnityEngine;
using TMPro;

public class OpenChatButton : PressableObject
{
    public ChatScript chat;
    public TMP_Text friendName;
    // kinda temperary will be heavily changed
    public override void Pressed()
    {
        Debug.Log("Hello");
        if (chat.gameObject.activeSelf) 
        {
            if (!chat.open) chat.Open(); else chat.Close(); 
        }
    }
}
