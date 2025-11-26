using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine.UIElements;

public class OSMechanics : MonoBehaviour
{
    public static OSMechanics mechInstance;
    static Dictionary<GameObject, (Vector3, Vector3)> minimized = new Dictionary<GameObject, (Vector3, Vector3)>();
    List<GameObject> resizing = new List<GameObject>();
    private void Start()
    {
        mechInstance = this;
    }
    public void OpenApp(GameObject app, Texture2D icon)
    {
        BottomOSBar.AddToBar(app, icon);
        app.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,100);
        ViewManager.AddToOrder(app);
        StartCoroutine(SizeAndMoveTo(app,app.transform.position,new Vector3(10,5,1),true));
    }
    public void MinimizeApp(GameObject app)
    {
        if (resizing.Contains(app)) return;
        minimized.Add(app, (app.transform.position, app.transform.localScale));
        Vector2 normSize = app.transform.localScale.normalized / 100;
        StartCoroutine(SizeAndMoveTo(app, BottomOSBar.apps[app].transform.position,new Vector3(normSize.x,normSize.y,1),false));
    }
    public void MaximizeApp(GameObject app) 
    {
        ViewManager.AddToOrder(app);
        if (resizing.Contains(app)) return;
        StartCoroutine(SizeAndMoveTo(app, minimized[app].Item1, minimized[app].Item2,true));
        minimized.Remove(app);
    }
    IEnumerator SizeAndMoveTo(GameObject app, Vector3 loc, Vector3 size, bool opening)
    {
        resizing.Add(app);
        if (opening) { app.SetActive(true); }
        int steps = 60;
        Vector3 scaleStep = (size-app.transform.localScale)/steps;
        Vector3 locStop = (loc-app.transform.position) / steps;
        for (int i = 0; i < steps; i++)
        {
            app.transform.localScale += scaleStep;
            app.transform.position += locStop;
            yield return null;
        }
        Debug.Log(size);
        app.transform.position = loc;
        app.transform.localScale = size;
       
        if (!opening) { app.SetActive(false); }
        resizing.Remove(app);
    }
    public static Vector2 ResizeImageToSize(Texture2D img, RectTransform rt, float size)
    {
        rt.gameObject.GetComponent<RawImage>().texture = img;
        rt.sizeDelta = new Vector2(img.width, img.height);
        return Vector2.one * (size / (float)MathF.Max(img.width, img.height));
    }
}
