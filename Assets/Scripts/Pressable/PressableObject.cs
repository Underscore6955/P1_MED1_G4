using UnityEngine;
using System.Collections;

public class PressableObject : MonoBehaviour
{
    public BoxCollider2D bc;
    float startHoldTime;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)) { Pressed(); startHoldTime = Time.time; }
        if (Input.GetMouseButtonUp(0)) Release();
    }
    public virtual void Pressed() { }
    public virtual void Release() { Release(Time.time - startHoldTime); }
    public virtual void Release(float time) { }
}
