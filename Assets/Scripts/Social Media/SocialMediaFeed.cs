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

    //Crazily enough? It starts the feed.
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
<<<<<<< HEAD
            GameObject curPost = SendPost(BuildNextPost(lines, i)); //Sends the current line of text to the SendPost method to create a post GameObject.
            curPost.transform.position = FindNextPos(curPost.transform.Find("Top").position.y - curPost.transform.Find("Bottom").position.y); //Sets the position of the current post using the FindNextPos method to determine the correct position based on the height of the post.
            if (i == 0) {scroll.origin = Instantiate(new GameObject(),transform).transform; scroll.contentTop = curPost.transform.Find("Top"); scroll.origin.position = scroll.contentTop.position; } //If this is the first post, it sets the origin and contentTop of the scrollable component to the top of the current post.
=======
            GameObject curPost = SendPost(BuildNextPost(lines, i));
            curPost.transform.position = FindNextPos(curPost.transform.Find("Top").position.y - curPost.transform.Find("Bottom").position.y);
            if (i == 0) { scroll.SetTop(curPost.transform.Find("Top"), curPost.transform.Find("Bottom"),true); }
>>>>>>> main
            scroll.contentBottom = curPost.transform.Find("Bottom");
            scroll.ScrollToPos();
            // once again weird z axis stuff
            curPost.transform.localPosition = new Vector3(curPost.transform.localPosition.x, curPost.transform.localPosition.y, 0);
        }
        scroll.curScroll = scroll.MaxScroll();
    }
    GameObject SendPost((string text, string name, Texture2D pfp, Texture2D img) data)
    {
        GameObject curPost = Instantiate(data.img ? postimagePrefab : posttextPrefab, content.transform); //Instantiates the post prefab using ternary operator to choose between image or text post.
        // place the post
        curPost.transform.localScale = OSMechanics.ResizeImageToSize((Texture2D)curPost.GetComponent<RawImage>().texture, curPost.GetComponent<RectTransform>(), 9f); //Uses the ResizeImageToSize method from OSMechanics to resize the post to fit within the feed.
        if (data.img)//If there was an image included in the post data, it sets the image component of the post to that image.
        {
            curPost.transform.Find("img").gameObject.GetComponent<RawImage>().texture = data.img;
        }
        curPost.transform.Find("pfp").gameObject.GetComponent<RawImage>().texture = data.pfp; //Finds the profile picture component of the post and sets its texture to the profile picture from the post data.
        curPost.transform.Find("username").gameObject.GetComponent<TMP_Text>().text = data.name; //Finds the username text component of the post and sets its text to the username from the post data.
        curPost.transform.Find("postText").gameObject.GetComponent<TMP_Text>().text = data.text; //Finds the post text component of the post and sets its text to the post text from the post data.
        return curPost;
    }
    // method to find the correct location of the next post
    Vector2 FindNextPos(float height)
    {
        return new Vector2 (transform.position.x,(scroll.contentBottom ? scroll.contentBottom.position.y - 0.2f - 0.5f * height : transform.position.y-2.5f) );
    }
    public static (string, string, Texture2D, Texture2D) BuildNextPost(string[] curLines, int line) //This method is mostly similar to BuildNextText from GetText.cs
    {
        string lineText = curLines[line]; //Gets the current line of text.
        string nameNameBuild = ""; //Variable to build the name.
        string textBuild = ""; //Variable to build the text.
        string imgNameBuild = null; //Variable to build the image.
        string pfpNameBuild = null; //Variable to build the profile picture.
        int building = 0; //A variable to keep track of what type of data it is currently building.
        for (int i = 2; i < lineText.Length-1; i++) //A for-loop to iterate through the files it is going to read.
        {
            if (lineText[i] == '|') { building++; continue; } //Here it looks for the "|" character to figure out what type of data it is looking at.
            if (building == 1) nameNameBuild += lineText[i]; //Depending on the value of "building", it appends the character to the appropriate string variable.
            else if (building == 2) pfpNameBuild += lineText[i]; //If building is 2 it builds the profile picture.
            else if (building ==3) imgNameBuild += lineText[i]; //If building is 3 it builds the image, and if there isnt one skips this part.
            else textBuild += lineText[i];
        }
<<<<<<< HEAD
        string path = "Social Media/profile pictures/"; //Sets the path for the images
        return ((textBuild,nameNameBuild, GetText.FindImg(path + pfpNameBuild),GetText.FindImg(path + imgNameBuild))); //The return value and which order the file must be written.
=======
        string path = "2dAssets/Social Media/profile pictures/";
        return ((textBuild.Replace("y/n","Frank"),nameNameBuild, GetText.FindImg(path + pfpfNameBuild),GetText.FindImg(path + imgNameBuild)));
>>>>>>> main
    }
}