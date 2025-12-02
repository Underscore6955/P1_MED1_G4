using UnityEngine;

public class SocialMediaFeed : MonoBehaviour
{
    [SerializeField] Scrollable scroll;
    [SerializeField] Canvas feedCanvas;
    [SerializeField] GameObject postPrefab;
    Transform contentBottom;
    void SendPost(string text, Texture2D img)
    {
        Instantiate(postPrefab, feedCanvas.transform);
        OSMechanics.ResizeImageToSize(img, postPrefab.GetComponent<RectTransform>(), 1f);

    }
    Vector2 FindNextPos()
    {
        return contentBottom.position + Vector3.down * 0.1f;
    }
}