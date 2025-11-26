using UnityEngine;
using UnityEngine.InputSystem;

public class Scrollable : MonoBehaviour
{
    [SerializeField] GameObject content;
    float curScroll = 0;
    Transform origin;
    public Transform contentTop;
    public Transform contentBottom;
    private void Start()
    {
        origin = Instantiate(new GameObject(),content.transform.parent).transform;
        origin.transform.position = content.transform.position;
    }
    private void Update()
    {
        if (contentTop) curScroll = Mathf.Clamp(curScroll + Input.mouseScrollDelta.y, 0, Mathf.Clamp(MaxScroll()-(0.5f*transform.localScale.y),0,Mathf.Infinity));
        if (contentTop) ScrollToPos(MaxScroll()-curScroll);
    }
    void ScrollToPos(float pos)
    {
        content.transform.position = new Vector2(content.transform.position.x, origin.position.y+pos);
    }
    float MaxScroll()
    {
        return (contentTop.position-contentBottom.position).y;
    }
}
