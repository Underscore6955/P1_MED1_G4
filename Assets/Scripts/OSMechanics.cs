using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OSMechanics : MonoBehaviour
{
    static Dictionary<GameObject, (Vector2, Vector2)> minimized = new Dictionary<GameObject, (Vector2, Vector2)>();
    public static void OpenApp(GameObject app)
    {

    }
    public static void MinimizeApp(GameObject app) 
    {
        minimized.Add(app, (app.transform.position, app.transform.localScale));
    }
    public static void MaximizeApp(GameObject app) 
    {
        
    }
    static void SizeAndMoveTo(GameObject app, Vector2 loc, Vector2 size)
    {

    }
    public static Vector2 ResizeImageToSize(Texture2D img, RectTransform rt, float size)
    {
        rt.gameObject.GetComponent<RawImage>().texture = img;
        rt.sizeDelta = new Vector2(img.width, img.height);
        return Vector2.one * (size / (float)MathF.Max(img.width, img.height));
    }
}
