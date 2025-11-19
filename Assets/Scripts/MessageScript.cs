using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour
{
    public string text;
    public Image image;
    public int players;
    public TMP_Text textField;
    public GameObject testBox;
    public Transform bottomPos;
    [SerializeField] GameObject cornerTL;
    [SerializeField] GameObject cornerTR;
    [SerializeField] GameObject cornerBL;
    [SerializeField] GameObject cornerBR;
    [SerializeField] GameObject leftFill;
    [SerializeField] GameObject rightFill;
    private void Start()
    {
        Vector2 size = FindSize(textField);
        testBox.transform.localScale = size;
        PlaceCorners(size);
    }

    Vector2 FindSize(TMP_Text text)
    {
        
        Vector2 size;
        size.x = text.renderedWidth/60f;
        size.y = (int)text.renderedHeight/60f;
        return size;
    }
    void PlaceCorners(Vector3 size)
    {
        Vector3 offset = new Vector3(0.128f, -0.064f);
        cornerBL.transform.position = (transform.position - size / 2f) - offset;
        cornerTL.transform.position = cornerBL.transform.position + new Vector3(0, size.y+2*offset.y);
        cornerBR.transform.position = cornerBL.transform.position + new Vector3(size.x+ 2 * offset.x,0);
        cornerTR.transform.position = cornerTL.transform.position + new Vector3(size.x+ 2 * offset.x, 0);

        leftFill.transform.localScale = new Vector2(0.2555555f, size.y+4*offset.y);
        leftFill.transform.position = cornerBL.transform.position + new Vector3(offset.x -0.5f* leftFill.transform.localScale.x, size.y*0.5f+offset.y);
        rightFill.transform.localScale = leftFill.transform.localScale;
        rightFill.transform.position = leftFill.transform.position + Vector3.right*(size.x+ leftFill.transform.localScale.x);
    }
}
