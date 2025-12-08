using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediaFeed : MonoBehaviour
{
    [SerializeField] Scrollable scroll;
    [SerializeField] Transform content;
    [SerializeField] GameObject postimagePrefab;
    [SerializeField] GameObject posttextPrefab;
    [SerializeField] TextAsset posts;
    // ask the expert :3
    private void Start()
    {
        InitiateFeed();
    }
    void InitiateFeed()
    {
        string[] lines = posts.text.Split('\n');
        for (int i = 0; i < lines.Length -1; i++)
        {
            // scrolly things
            GameObject curPost = SendPost(BuildNextPost(lines, i));
            curPost.transform.position = FindNextPos(curPost.transform.Find("Top").position.y - curPost.transform.Find("Bottom").position.y);
            if (i == 0) { scroll.SetTop(curPost.transform.Find("Top"), curPost.transform.Find("Bottom")); }
            scroll.contentBottom = curPost.transform.Find("Bottom");
            // once again weird z axis stuff
            curPost.transform.localPosition = new Vector3(curPost.transform.localPosition.x, curPost.transform.localPosition.y, 0);
        }
        scroll.curScroll = scroll.MaxScroll() - scroll.firstSize;
    }
    GameObject SendPost((string text, string name, Texture2D pfp, Texture2D img) data)
    {
        GameObject curPost = Instantiate(data.img ? postimagePrefab : posttextPrefab, content.transform);
        // place the post
        curPost.transform.localScale = OSMechanics.ResizeImageToSize((Texture2D)curPost.GetComponent<RawImage>().texture, curPost.GetComponent<RectTransform>(), 9f);
        if (data.img)
        {
            curPost.transform.Find("img").gameObject.GetComponent<RawImage>().texture = data.img;
        }
        curPost.transform.Find("pfp").gameObject.GetComponent<RawImage>().texture = data.pfp;
        curPost.transform.Find("username").gameObject.GetComponent<TMP_Text>().text = data.name;
        curPost.transform.Find("postText").gameObject.GetComponent<TMP_Text>().text = data.text;
        return curPost;
    }
    // method to find the correct location of the next post
    Vector2 FindNextPos(float height)
    {
        return new Vector2 (transform.position.x,(scroll.contentBottom ? scroll.contentBottom.position.y - 0.2f : transform.position.y-2.5f) - 0.5f*height);
    }
    public static (string, string, Texture2D, Texture2D) BuildNextPost(string[] curLines, int line)
    {
        string lineText = curLines[line];
        string nameNameBuild = "";
        string textBuild = "";
        string imgNameBuild = null;
        string pfpfNameBuild = null;
        int building = 0;
        for (int i = 2; i < lineText.Length-1; i++)
        {
            if (lineText[i] == '|') { building++; continue; }
            if (building == 1) nameNameBuild += lineText[i];
            else if (building == 2) pfpfNameBuild += lineText[i];
            else if (building ==3) imgNameBuild += lineText[i];
            else textBuild += lineText[i];
        }
        string path = "Social Media/profile pictures/";
        return ((textBuild,nameNameBuild, GetText.FindImg(path + pfpfNameBuild),GetText.FindImg(path + imgNameBuild)));
    }
}