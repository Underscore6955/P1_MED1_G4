using UnityEngine;

public class MinimizeButton : PressableObject
{
    [SerializeField] GameObject app;
    // very self explanotory if you read any other button
    public override void Release()
    {
        OSMechanics.mechInstance.MinimizeApp(app);
    }
}
