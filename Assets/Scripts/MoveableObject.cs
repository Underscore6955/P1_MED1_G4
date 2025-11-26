using UnityEngine;
using System.Collections;

public class MoveableObject : PressableObject
{
    public override void Pressed()
    {
        StartCoroutine(Holding());
    }
    public override void Release()
    {
        StopAllCoroutines();
    }
    IEnumerator Holding()
    {
        Vector3 lastMousePos;
        while (true)
        {
            lastMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            yield return null;
            transform.position += Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePos;
        }
    }
}
