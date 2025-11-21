using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour
{
    public Image image;
    public int players;
    public TMP_Text textField;
    public GameObject testBox;
    public Transform bottomPos;
    public Transform topPos;
    [SerializeField] GameObject cornerTL;
    [SerializeField] GameObject cornerTR;
    [SerializeField] GameObject cornerBL;
    [SerializeField] GameObject cornerBR;
    [SerializeField] GameObject leftFill;
    [SerializeField] GameObject rightFill;
    public Transform width;
    public void Sizing()
    {
        Vector2 size = FindSize(textField);
        testBox.transform.localScale = size;
        PlaceCorners(size);
    }
    public void CheckIfPlayers()
    {
        if (players == 1)
        {
            foreach (Image img in transform.parent.GetComponentsInChildren<Image>())
            {
                img.color = new Color(144f/256f, 238f / 256f, 144f / 256f);
            }
        }
        else
        {
            Image imgBL = cornerBL.GetComponent<Image>();
            Image imgBR = cornerBR.GetComponent<Image>();
            Sprite spriteBL = imgBL.sprite;
            imgBL.sprite = imgBR.sprite;
            imgBR.sprite = spriteBL;
            imgBL.transform.localScale = Vector3.Scale(imgBL.transform.localScale,new Vector3(-1,1,1));
            imgBR.transform.localScale = Vector3.Scale(imgBR.transform.localScale, new Vector3(-1, 1, 1));
        }
    }

    public static Vector2 FindSize(TMP_Text text)
    {   
        Vector2 size;
        size.x = text.renderedWidth/(1/text.rectTransform.localScale.y);
        size.y = (int)text.renderedHeight / (1 / text.rectTransform.localScale.y);
        return size;
    }
    void PlaceCorners(Vector3 size)
    {
        RectTransform cornerRT = cornerTL.GetComponent<RectTransform>();
        Vector3 offset = new Vector3(0.4f*cornerRT.rect.width, -0.2f * cornerRT.rect.width);
        cornerBL.transform.position = (transform.position - size / 2f) - offset;
        cornerTL.transform.position = cornerBL.transform.position + new Vector3(0, size.y+2*offset.y);
        cornerBR.transform.position = cornerBL.transform.position + new Vector3(size.x+ 2 * offset.x,0);
        cornerTR.transform.position = cornerTL.transform.position + new Vector3(size.x+ 2 * offset.x, 0);

        leftFill.transform.localScale = new Vector2(cornerRT.rect.width*cornerRT.localScale.x, size.y+4*offset.y);
        leftFill.transform.position = cornerBL.transform.position + new Vector3(offset.x -0.5f* leftFill.transform.localScale.x, size.y*0.5f+offset.y);
        rightFill.transform.localScale = leftFill.transform.localScale;
        rightFill.transform.position = leftFill.transform.position + Vector3.right*(size.x+ leftFill.transform.localScale.x);
    }
}
