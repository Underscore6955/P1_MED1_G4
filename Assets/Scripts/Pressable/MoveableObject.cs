using UnityEngine;
using System.Collections;

public class MoveableObject : PressableObject
{
    // since this is a pressable object, as seen in the class definition, the override pressed will run whenever you press this collider
    public override void Pressed()
    {
        // we start the moving the object to the mouse
        StartCoroutine(Holding());
        // put this in front, see viewmanager
        ViewManager.AddToOrder(gameObject);
    }
    // when you stop holding the mouse, it needs to stop moving this to the mouse
    public override void Release()
    {
        StopAllCoroutines();
    }
    IEnumerator Holding()
    {
        // it tracks the difference between where your mouse was last frame and where it is now, and moves the gameobject accordingly
        Vector3 lastMousePos;
        while (true)
        {
            lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return null;
            transform.position += Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePos;
        }
    }
}
