using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MessageScript : MonoBehaviour
{
    public Texture2D image;
    public int players;
    public TMP_Text textField;
    public GameObject testBox;
    // used to track the size, and bottom and top position of the message, assigned in prefab and they then scale with the message, and are always placed right
    public Transform bottomPos;
    public Transform topPos;
    public Transform width;
    // prooooobbabbllyy should be seralized but i cant be bothered
    static int imgSize = 2;
    // corners, assigned in prefab
    [SerializeField] GameObject cornerTL;
    [SerializeField] GameObject cornerTR;
    [SerializeField] GameObject cornerBL;
    [SerializeField] GameObject cornerBR;
    [SerializeField] GameObject leftFill;
    [SerializeField] GameObject rightFill;
    [SerializeField] GameObject imgPrefab;
    RectTransform curImg;
    // method used to create image
    public void BuildImg()
    {
        // make a new img prefab, make it a child of the text message and get its recttransform
        curImg = Instantiate(imgPrefab, transform.parent).GetComponent<RectTransform>();
        // make it the correct size and put the image into the gameobject
        curImg.localScale = OSMechanics.ResizeImageToSize(image,curImg,imgSize);
    }
    // places the image correctly
    public void FindImgPos(ChatScript chat)
    {
        // does a little funky math manuvere and places it according to the size of the image and what side of the screen it is on (players)
        Vector2 loc = chat.SMS.FindNextNotFirst(transform.parent.parent.parent.parent.localScale.x/10f*(curImg.sizeDelta.x * curImg.localScale.x), chat.content.transform.position.x, players, (curImg.sizeDelta.y * curImg.localScale.y)); ;
        // the bottom of the message should now be the bottom of the image, not the text
        bottomPos = curImg.Find("Bottom");
        curImg.transform.position = loc;
    }
    // sizes the object according to the text
    public void Sizing()
    {
        // finds the correct size and applies it
        Vector2 size = FindSize(textField);
        testBox.transform.localScale = size;
        // places the corners, cornerBL will always be true here, just ignore the if statement
        if (cornerBL) PlaceCorners(size * transform.parent.parent.parent.parent.localScale.x/10, size);
    }
    // we need to check who the message was sent by
    public void CheckIfPlayers()
    {
        // if the player sent the message, make all images in the message green
        if (players == 1)
        {
            foreach (Image img in transform.parent.GetComponentsInChildren<Image>())
            {
                img.color = new Color(144f/256f, 238f / 256f, 144f / 256f);
            }
        }
        // if it is the other guy, move the corner around a bit
        else
        {
            Image imgBL = cornerBL.GetComponent<Image>();
            Image imgBR = cornerBR.GetComponent<Image>();
            Sprite spriteBL = imgBL.sprite;
            imgBL.sprite = imgBR.sprite;
            imgBR.sprite = spriteBL;
            imgBL.transform.localScale = Vector3.Scale(imgBL.transform.localScale,new Vector3(-1,1,1));
            imgBR.transform.localScale = Vector3.Scale(imgBR.transform.localScale, new Vector3(-1, 1, 1));
            imgBR.GetComponent<RectTransform>().pivot = Vector2.right;
            imgBL.GetComponent<RectTransform>().pivot = Vector2.zero;

        }
    }
    // find the correct size of the box that fits around the text
    public static Vector2 FindSize(TMP_Text text)
    {   
        Vector2 size;
        size.x = text.renderedWidth*text.rectTransform.localScale.y;
        size.y = (int)text.renderedHeight*text.rectTransform.localScale.y;
        return size;
    }
    void PlaceCorners(Vector3 size, Vector3 unscaledSize)
    {
        // this is just a bunch of boring math, but it works
        RectTransform cornerRT = cornerTL.GetComponent<RectTransform>();
        cornerBL.transform.position = (transform.position - size / 2f);
        cornerTL.transform.position = cornerBL.transform.position + new Vector3(0, size.y);
        cornerBR.transform.position = cornerBL.transform.position + new Vector3(size.x,0);
        cornerTR.transform.position = cornerTL.transform.position + new Vector3(size.x, 0);

        leftFill.transform.localScale = new Vector2(cornerRT.rect.width*cornerRT.localScale.x, Mathf.Clamp(unscaledSize.y-cornerRT.rect.height*0.8f*2f,0,Mathf.Infinity));
        leftFill.transform.position = cornerBL.transform.position+Vector3.up * cornerRT.rect.height*0.8f*(transform.parent.parent.parent.parent.localScale.y / 5);
        rightFill.transform.localScale = leftFill.transform.localScale;
        rightFill.transform.position = leftFill.transform.position + Vector3.right*(size.x);
    }
}
