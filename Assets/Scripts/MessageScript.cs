using UnityEngine;
using TMPro;

public class MessageScript : MonoBehaviour
{
    public string text;
    [SerializeField] TMP_Text textField;
    [SerializeField] GameObject testBox;
    [SerializeField] GameObject cornerTL;
    [SerializeField] GameObject cornerTR;
    [SerializeField] GameObject cornerBL;
    [SerializeField] GameObject cornerBR;
    [SerializeField] GameObject leftFill;
    [SerializeField] GameObject rightFill;

    void Start()
    {
        text = "Hello everybody my name is markiplier and welcome to the worlds quietest letsplay";
        textField.text = text;
        
    }
    private void Update()
    {
        Debug.Log(textField.renderedWidth);
        Vector2 size = FindSize(textField);
        testBox.transform.localScale = size;
        PlaceCorners(size);
    }

    Vector2 FindSize(TMP_Text text)
    {
        Vector2 size;
        size.x = 0.2f+text.renderedWidth/60f;
        size.y = (int)text.renderedHeight/60f;
        return size;
    }
    void PlaceCorners(Vector3 size)
    {
        float offset = 0.2f;
        cornerBL.transform.position = (transform.position - size/2f) + Vector3.left*offset;
        cornerTL.transform.position = cornerBL.transform.position + new Vector3(0, size.y);
        cornerBR.transform.position = cornerBL.transform.position + new Vector3(size.x+ 2 * offset,0);
        cornerTR.transform.position = cornerTL.transform.position + new Vector3(size.x+ 2 * offset, 0);

        leftFill.transform.localScale = new Vector2(0.1f,size.y-offset);
        leftFill.transform.position = cornerBL.transform.position + new Vector3(offset-0.05f, 0.5f*size.y);
        rightFill.transform.localScale = leftFill.transform.localScale;
        rightFill.transform.position = leftFill.transform.position + Vector3.right*(size.x+ 0.1f);
    }
}
