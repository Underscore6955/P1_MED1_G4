using UnityEngine;
using UnityEngine.UI;

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
