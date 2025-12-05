using UnityEngine;
using UnityEngine.UI;

public class AppShortcut : PressableObject
{
    public GameObject app;
    [SerializeField] Texture2D icon;
    public bool opened;
    private void Start()
    {
        // oh this explains why bottombaros doesnt work lol it just gets overwritten here ill fix that sometime
        GetComponent<RawImage>().texture = icon;
    }
    public override void Pressed()
    {
        // if the app hasnt been opened, we open it
        if (!opened)
        {
            OSMechanics.mechInstance.OpenApp(app, icon);
        }
        else
        {
            // otherwise we minimize or maximize it, depending on what it was before
            if (app.activeSelf)
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
}
