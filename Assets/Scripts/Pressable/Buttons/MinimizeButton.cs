using UnityEngine;

public class MinimizeButton : PressableObject
{
    [SerializeField] GameObject app;
    public override void Release()
    {
        OSMechanics.mechInstance.MinimizeApp(app);
    }
}
