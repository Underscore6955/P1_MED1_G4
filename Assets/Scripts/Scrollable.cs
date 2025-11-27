using UnityEngine;
using UnityEngine.InputSystem;

public class Scrollable : MonoBehaviour
{
    [SerializeField] GameObject content;
    float curScroll = 0;
    Transform origin;
    public Transform contentTop;
    public Transform contentBottom;
    [SerializeField] Collider2D scrollArea;
    private void Start()
    {
        origin = Instantiate(new GameObject(),content.transform.parent).transform;
        origin.transform.position = content.transform.position;
    }
    private void Update()
    {
        if (Input.mouseScrollDelta.y != 0 && IsHovered()) FindScroll();
        if (contentTop) ScrollToPos(MaxScroll() - curScroll);
    }
    void FindScroll()
    {
        if (contentTop) curScroll = Mathf.Clamp(curScroll + Input.mouseScrollDelta.y, 0, Mathf.Clamp(MaxScroll() - (0.5f * transform.localScale.y), 0, Mathf.Infinity));
    }
    void ScrollToPos(float pos)
    {
        content.transform.position = new Vector2(content.transform.position.x, origin.position.y + pos);
    }
    float MaxScroll()
    {
        return (contentTop.position-contentBottom.position).y;
    }
    bool IsHovered()
    {
        foreach (Collider2D curCol in Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
        {
            try { if (curCol.transform.parent.gameObject.GetComponent<Scrollable>()) return curCol == scrollArea; } catch { }
        }
        return false;
    }
}
