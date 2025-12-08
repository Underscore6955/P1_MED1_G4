using UnityEngine;
using UnityEngine.InputSystem;

public class Scrollable : MonoBehaviour
{
    public GameObject content;
    public float curScroll = 0;
    public Transform origin;
    public Transform contentTop;
    public Transform contentBottom;
    [SerializeField] Collider2D scrollArea;
    private void Update()
    {
        // if there is nothing to scroll in dont scroll
        if (content == null) return;
        // if you are scrolling, and your mouse is over the thing, we can change where your scroll is
        if (Input.mouseScrollDelta.y != 0 && IsHovered()) FindScroll();
        // scroll to the correct location
        if (contentTop) ScrollToPos(MaxScroll() - curScroll);
    }
    // does some funky math stuff to find where you can scroll
    void FindScroll()
    {
        if (contentTop) curScroll = Mathf.Clamp(curScroll + Input.mouseScrollDelta.y, 0, Mathf.Clamp(MaxScroll() - (0.5f * transform.localScale.y), 0, Mathf.Infinity));
    }
    // scrolls to the position that it needs to, that means moving the content properly 
    void ScrollToPos(float pos)
    {
        content.transform.position = new Vector2(content.transform.position.x, origin.position.y + pos);
    }
    // finds the maximum distance you can scroll
    float MaxScroll()
    {
        return (contentTop.position-contentBottom.position).y;
    }
    // checks if the mouse is over the thing, and it is the scrollable that is highest in the sorting order
    bool IsHovered()
    {
        foreach (Collider2D curCol in Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            // checks each gameobject under the mouse for scrollable script, and the first one it finds will be the one closest to the camerea, which will be the top one
            // if the top scroll is this one, then that means this should return true
            // if there is no scrollable, or the wrong scollable is on top, we return false
            try { if (curCol.transform.parent.gameObject.GetComponent<Scrollable>()) return curCol == scrollArea; } catch { }
        }
        return false;
    }
}
