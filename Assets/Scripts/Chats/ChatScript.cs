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
    [HideInInspector] public Transform topScroll;
    [HideInInspector] public Transform bottomScroll;

    public TextAsset textFile;
    public TextAsset choiceFile;
    [HideInInspector] public bool choosing = false;
    public string dataFileName;

    public static GameObject messagePrefab;
    public static GameObject choiceButtonPrefab;
    [SerializeField] GameObject messagePrefabAssign;
    [SerializeField] GameObject choiceButtonPrefabAssign;

    bool started;
    public bool open {  get; private set; }

    [SerializeField] Scrollable scroll;
    private void Awake()
    {
        SMS = new SendMessageScript(this);
        CT = new ChoiceTracker(this);
        GT = new GetText(this);
        if(messagePrefabAssign) messagePrefab = messagePrefabAssign;
        if(choiceButtonPrefabAssign) choiceButtonPrefab = choiceButtonPrefabAssign;
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
        choiceCanvas.gameObject.SetActive(false);
        content.position -= Vector3.left*100f;
    }
}
