using UnityEngine;

public class CloseButton : PressableObject
{
    public override void Release()
    {
        Application.Quit();
    }
}
