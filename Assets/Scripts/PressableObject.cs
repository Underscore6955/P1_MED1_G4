using UnityEngine;

public class PressableObject : MonoBehaviour
{
    BoxCollider2D bc;
    bool holdable;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
    }
    private void OnMouseOver()
    {
        if (holdable)
        {
            if (Input.GetMouseButton(0)) Pressed();
        }
        else
        {
            if (Input.GetMouseButtonUp(0)) Pressed();
        }
    }
    public virtual void Pressed()
    {

    }
}
