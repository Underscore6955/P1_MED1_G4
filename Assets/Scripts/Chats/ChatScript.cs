using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
using static UnityEngine.UI.Image;

public class ChatScript : MonoBehaviour 
{
    // bunch of variables we need, many from ChatApp
    public SendMessageScript SMS;
    public GetText GT;
    public ChoiceTracker CT;
    public ScaleScript SC;

    public Transform choiceCanvas;

    public Transform content;
    public float xOffset;
    public Transform topScroll;
    public Transform bottomScroll;
    public float choiceOffset;

    public TextAsset textFile;
    public TextAsset choiceFile;
    public bool choosing = false;
    public string dataFileName;
    public string friendName;
    public static string yourName;

    public GameObject messagePrefab;
    public GameObject choiceButtonPrefab;
    public GameObject textBar;
    
    public AudioSource AS; 

    // we need some bools to keep track of whether the chat is open, and whether it has been opened before
    bool started;
    public bool open {  get; private set; }

    public Scrollable scroll;
    // this method prepares the chat to be used
    public void InitiateChat()
    {
        // uses the constructors on the scripts to make the new scripts
        SMS = new SendMessageScript(this);
        CT = new ChoiceTracker(this);
        GT = new GetText(this);
        // app starts closed
        Close();
    }
    public void Open()
    {
        // we move the chat back to the app, see why in Closed()
        ChatApp.ChangeChat(this);
        content.localPosition = new Vector3(0, content.localPosition.y, content.localPosition.z);
        open = true;
        // if it is the first time we open the app we start the chat
        if (!started)
        {
            // make sure this doesnt go again
            started = true;
            // begin the chat
            GT.StartChat(); 
        }
        // make sure that everything is on and correctly displayed 
        textBar.SetActive(true);
        scroll.enabled = true;
        scroll.content = content.gameObject;
        scroll.contentTop = topScroll;
        scroll.contentBottom = bottomScroll;
        // scroll to the bottom
        scroll.curScroll = 0;
        choiceCanvas.gameObject.SetActive(true);
    }
    public void Close()
    {
        // hide everything
        open = false;
        scroll.enabled = false;
        textBar.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        // it is very important that the messages still exist, and are rendered even when the chat is off, since a message can still be sent when the chat is disabled
        // this means that if the canvas is disabled, the text is not rendered and will therefore be -infinity large for whatever reason
        // so instead of disabling it we just kinda push it to the side
        content.position += Vector3.left * 100f;
    }
}
