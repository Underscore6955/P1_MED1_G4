using UnityEngine;

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

    [SerializeField] Scrollable scroll;
    private void Awake()
    {
        SMS = new SendMessageScript(this);
        CT = new ChoiceTracker(this);
        GT = new GetText(this);
        if(messagePrefabAssign) messagePrefab = messagePrefabAssign;
        if(choiceButtonPrefabAssign) choiceButtonPrefab = choiceButtonPrefabAssign;
        GT.StartChat();
    }
    private void OnEnable()
    {
        scroll.enabled = true;
        scroll.contentTop = topScroll;
        scroll.contentBottom = bottomScroll;
    }
    private void OnDisable()
    {
        scroll.enabled = false;   
    }
}
