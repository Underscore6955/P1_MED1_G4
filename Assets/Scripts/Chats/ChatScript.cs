using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static UnityEngine.UI.Image;

public class ChatScript : MonoBehaviour 
{
    public SendMessageScript SMS;
    public GetText GT;
    public ChoiceTracker CT;

    public Transform choiceCanvas;

    public Transform content;
    public float xOffset;
    public Transform topScroll;
    public Transform bottomScroll;

    public TextAsset textFile;
    public TextAsset choiceFile;
    public bool choosing = false;
    public string dataFileName;

    public GameObject messagePrefab;
    public GameObject choiceButtonPrefab;
    public GameObject textBar;

    bool started;
    public bool open {  get; private set; }

    public Scrollable scroll;
    public void InitiateChat()
    {
        SMS = new SendMessageScript(this);
        CT = new ChoiceTracker(this);
        GT = new GetText(this);
        Close();
    }
    public void Open()
    {
        content.position += Vector3.left * 100f;
        open = true;
        if (!started)
        {
            scroll.origin = Instantiate(new GameObject(), content.transform.parent).transform;
            started = true;
            scroll.origin.transform.position = content.transform.position;
            GT.StartChat(); 
        }
        textBar.SetActive(true);
        scroll.enabled = true;
        scroll.content = content.gameObject;
        scroll.contentTop = topScroll;
        scroll.contentBottom = bottomScroll;
        scroll.curScroll = 0;
        choiceCanvas.gameObject.SetActive(true);
    }
    public void Close()
    {
        open = false;
        scroll.enabled = false;
        textBar.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        content.position -= Vector3.left*100f;
    }
}
