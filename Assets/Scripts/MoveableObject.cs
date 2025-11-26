using UnityEngine;
using System.Collections;

public class MoveableObject : PressableObject
{

    public override void Pressed()
    {
        
    }
    public override void Release(float time)
    {
        Debug.Log(time);
    }
}
