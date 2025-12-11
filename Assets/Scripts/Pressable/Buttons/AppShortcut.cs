using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class AppShortcut : PressableObject
{
    public GameObject app;
    public bool opened;
    public static Dictionary<GameObject,List<AppShortcut>> shortcuts = new Dictionary<GameObject,List<AppShortcut>>();
    [SerializeField] RawImage newNotif;
    public void Start()
    {
        if (!shortcuts.ContainsKey(app)) { shortcuts.Add(app, new List<AppShortcut>()); }
        shortcuts[app].Add(this);
    }
    public static void UpdateNotifs(GameObject updateApp, int notifs)
    {
        foreach (AppShortcut AS in shortcuts[updateApp]) { Debug.Log("hello"); AS.newNotif.enabled = notifs > 0; }
    }
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
        curChat.GT.StartChat();
    }
}
