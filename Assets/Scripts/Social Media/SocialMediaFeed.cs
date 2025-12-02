using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediaFeed : MonoBehaviour
{
    [SerializeField] Scrollable scroll;
    [SerializeField] Canvas feedCanvas;
    [SerializeField] GameObject postimagePrefab;
    [SerializeField] GameObject posttextPrefab;
    [SerializeField] TextAsset posts;
    Transform contentBottom;

    private void Awake()
    {
        InitiateFeed();
    }
    void InitiateFeed()
    {
        string[] lines = posts.text.Split('\n');
        for(int i = 0; i < lines.Length; i++)
        {
            SendPost(BuildNextPost(lines,i));
        }
    }
    void SendPost((string text, string name, Texture2D pfp, Texture2D img) data)
    {
        Debug.Log(data.text + " " + data.name + " " + data.pfp + " " + data.img);
        GameObject currPost = Instantiate(data.img ? postimagePrefab : posttextPrefab, feedCanvas.transform);
        currPost.transform.localScale = OSMechanics.ResizeImageToSize((Texture2D)currPost.GetComponent<RawImage>().texture, currPost.GetComponent<RectTransform>(), 1f);
        if (data.img)
        {
            currPost.transform.Find("img").gameObject.GetComponent<RawImage>().texture = data.img;
        }
        currPost.transform.Find("pfp").gameObject.GetComponent<RawImage>().texture = data.pfp;
        currPost.transform.Find("username").gameObject.GetComponent<TMP_Text>().text = data.name;
        currPost.transform.Find("postText").gameObject.GetComponent<TMP_Text>().text = data.text;
    }
    Vector2 FindNextPos()
    {
        return contentBottom.position + Vector3.down * 0.1f;
    }
    public static (string, string, Texture2D, Texture2D) BuildNextPost(string[] curLines, int line)
    {
        string lineText = curLines[line];
        string nameNameBuild = "";
        string textBuild = "";
        string imgNameBuild = null;
        string pfpfNameBuild = null;
        int building = 0;
        for (int i = 2; i < lineText.Length; i++)
        {
            Debug.Log(building + " " +  lineText[i]);
            if (lineText[i] == '|') { building++; continue; }
            if (building == 1) nameNameBuild += lineText[i];
            else if (building == 2) pfpfNameBuild += lineText[i];
            else if (building ==3) imgNameBuild += lineText[i];
            else textBuild += lineText[i];
        }
        return ((textBuild,nameNameBuild, GetText.FindImg(pfpfNameBuild),GetText.FindImg(imgNameBuild)));
    }
}