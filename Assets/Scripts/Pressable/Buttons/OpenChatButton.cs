using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OpenChatButton : PressableObject
{
    public ChatScript chat;
    public TMP_Text friendName;
    public RawImage newNotif;
    // kinda temperary will be heavily changed
    public override void Pressed()
    {
        if (chat.gameObject.activeSelf) 
        {
            if (!chat.open) chat.Open(); else chat.Close(); 
        }
    }
}
