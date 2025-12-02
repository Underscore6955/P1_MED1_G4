using UnityEngine;
using UnityEngine.UI;

public class AppShortcut : PressableObject
{
    public GameObject app;
    [SerializeField] Texture2D icon;
    public bool opened;
    private void Start()
    {
        GetComponent<RawImage>().texture = icon;
    }
    public override void Pressed()
    {
        if (!opened)
        {
            OSMechanics.mechInstance.OpenApp(app, icon);
            ViewManager.AddToOrder(app);
        }
        else
        {
            if (app.activeSelf)
            {
                OSMechanics.mechInstance.MinimizeApp(app);
            }
            else
            {
                OSMechanics.mechInstance.MaximizeApp(app);
            }
        }
        opened = true;
    }
}
