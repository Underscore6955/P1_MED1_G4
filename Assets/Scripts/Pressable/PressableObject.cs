using UnityEngine;
using System.Collections;

// this script is a parent class for many other scripts
public class PressableObject : MonoBehaviour
{
    public BoxCollider2D bc;
    float startHoldTime;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }
    // runs whenever the mouse is over this objects collider
    private void OnMouseOver()
    {
        // check if you are pressing the mouse
        if (Input.GetMouseButtonDown(0)) { Pressed(); startHoldTime = Time.time; }
        if (Input.GetMouseButtonUp(0)) Release();
    }
    // uses overloading, but rn there is no use for Release(float)
    public virtual void Pressed() { }
    public virtual void Release() { Release(Time.time - startHoldTime); }
    public virtual void Release(float time) { }
}
