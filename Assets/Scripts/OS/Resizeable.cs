using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

// ignore this script it fucking SUCKS </3 it kindaaa works but only barely 
// look at moveable object instead
public class Resizeable : PressableObject
{
    RectTransform sizingObject;
    Vector2 startMousePos;
    Vector2 startSize;
    private void Start()
    {
        sizingObject = transform.parent.GetComponent<RectTransform>();
    }
    public override void Pressed()
    {
        startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        startSize = sizingObject.localScale;
        StartCoroutine(Holding());
        ViewManager.AddToOrder(transform.parent.gameObject);
    }
    IEnumerator Holding()
    {
        while (true)
        {
            Vector2 moveDif = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - startMousePos;
            float scaleFactor = Mathf.Min(moveDif.x, moveDif.y);
            Vector2 scaleBy = scaleFactor < 0 ? startSize.normalized * 2f * scaleFactor : startSize.normalized * 4f * scaleFactor;
            Vector2 newSize = startSize + scaleBy;
            sizingObject.localScale = new Vector3(Mathf.Clamp(newSize.x,startSize.x/10,Mathf.Infinity), Mathf.Clamp(newSize.y, startSize.y / 10, Mathf.Infinity), 1);
            if (Input.GetMouseButtonUp(0) || Input.mousePosition.y > Screen.height) yield break;
            yield return null;
        }
    }
}
