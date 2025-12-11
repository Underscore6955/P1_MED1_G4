using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AppShortcut : PressableObject
{
    public GameObject app;
    public bool opened;
    public override void Pressed()
    {
        // if the app hasnt been opened, we open it
        if (!opened)
        {
            OSMechanics.mechInstance.OpenApp(app, (Texture2D)GetComponent<RawImage>().texture);
            if (app.name == "SoMeApp") StartCoroutine(StartChat());
        }
        else
        {
            // otherwise we minimize or maximize it, depending on what it was before
            if (!OSMechanics.minimized.ContainsKey(app))
            {
                OSMechanics.mechInstance.MinimizeApp(app);
            }
            else
            {
                OSMechanics.mechInstance.MaximizeApp(app);
            }
        }
        // make sure it remembers it has been opened
        // yk thinking about it i dont think this work properly, cus it needs to be app.opened or whatever instead
        opened = true;
    }
    IEnumerator StartChat()
    {
        yield return new WaitForSeconds(5);
        ChatScript curChat = ChatApp.chatInstance.AddChat(OSMechanics.mechInstance.jackText, OSMechanics.mechInstance.jackChoice);
        Debug.Log(curChat);
        curChat.GT.StartChat();
    }
}
