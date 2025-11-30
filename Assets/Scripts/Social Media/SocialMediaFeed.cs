using UnityEngine;

public class SocialMediaFeed : MonoBehaviour
{
    [SerializeField] Scrollable scroll;
    [SerializeField] Canvas feedCanvas;
    Transform contentBottom;
    void SendPost()
    {

    }
    Vector2 FindNextPos()
    {
        return contentBottom.position + Vector3.down*0.1f;
    }
}
