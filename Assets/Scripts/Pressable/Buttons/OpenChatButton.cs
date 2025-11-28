using UnityEngine;

public class OpenChatButton : PressableObject
{
    [SerializeField] ChatScript chat;
    public override void Pressed()
    {
        if (chat.gameObject.activeSelf) 
        {
            if (!chat.open) chat.Open(); else chat.Close(); 
        }
    }
}
