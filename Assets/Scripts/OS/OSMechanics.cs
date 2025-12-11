using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine.UIElements;

public class OSMechanics : MonoBehaviour
{
    // we make this script static kinda, just makes it easier to refer to in other places
    public static OSMechanics mechInstance;
    // we have a list of minimized apps, with their original locations and sizes stored
    public static Dictionary<GameObject, (Vector3, Vector3)> minimized = new Dictionary<GameObject, (Vector3, Vector3)>();
    public TextAsset jackText;
    public TextAsset jackChoice;
    // remember which things it is resizing currently, cuz if you try to resize something multiple times at once it creates conflict
    List<GameObject> resizing = new List<GameObject>();
    [SerializeField] RectTransform desktopCanvas;
    private void Start()
    {
        mechInstance = this;
        // same as bottomosbar
        desktopCanvas.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }
    // method to open app for the first time
    public void OpenApp(GameObject app, Texture2D icon)
    {
        BottomOSBar.AddToBar(app, icon);
        // place the app semi-correctly
        app.transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,100);
        // add it to the vieworder (see viewmanager and moveable object)
        ViewManager.AddToOrder(app);
        // moves it to where it actually should be
        StartCoroutine(SizeAndMoveTo(app,app.transform.position,new Vector3(10,5,1),true));
    }
    // method for minimizing app
    public void MinimizeApp(GameObject app)
    {
        // if it is already being resized, dont do anything
        if (resizing.Contains(app)) return;
        // make sure it remembers where it was and how big it was
        minimized.Add(app, (app.transform.position, app.transform.localScale));
        // get the correct size, because it needs to not scale in z
        Vector2 normSize = app.transform.localScale.normalized / 100;
        // move it to the bottom bar shortcut and make it small 
        StartCoroutine(SizeAndMoveTo(app, BottomOSBar.apps[app].transform.position,new Vector3(normSize.x,normSize.y,1),false));
    }
    // pretty much just the exact reverse of minimize
    public void MaximizeApp(GameObject app) 
    {
        ViewManager.AddToOrder(app);
        if (resizing.Contains(app)) return;
        StartCoroutine(SizeAndMoveTo(app, minimized[app].Item1, minimized[app].Item2,true));
        minimized.Remove(app);
    }
    // method that Sizes an object... and moves it to somewhere... no way
    IEnumerator SizeAndMoveTo(GameObject app, Vector3 loc, Vector3 size, bool opening)
    {
        resizing.Add(app);
        // if the app is being maximized we show it
        if (opening) { app.gameObject.transform.position -= Vector3.right * 500; }
        // amount of frames it should take this is good on my pc, but kinda slow for others cough cough
        int steps = 60;
        // find out how much it needs to move, to get to the correct place
        Vector3 scaleStep = (size-app.transform.localScale)/steps;
        Vector3 locStop = (loc-app.transform.position) / steps;
        // move it steps times toward where it needs to be and its size
        for (int i = 0; i < steps; i++)
        {
            app.transform.localScale += scaleStep;
            app.transform.position += locStop;
            yield return null;
        }
        // unity makes rounding errors so we make it exact
        app.transform.position = loc;
        app.transform.localScale = size;
       // if the app has been minimized we hide it
        if (!opening) { app.gameObject.transform.position += Vector3.right * 500; }
        resizing.Remove(app);
    }
    // method used a few times for making images correct
    // apparantly it doesn't fully work?? idk??
    public static Vector2 ResizeImageToSize(Texture2D img, RectTransform rt, float size)
    {
        rt.gameObject.GetComponent<RawImage>().texture = img;
        // make the image the correct ratio
        rt.sizeDelta = new Vector2(img.width, img.height);
        // make the image the correct size
        return Vector2.one * (size / (float)MathF.Max(img.width, img.height));
    }
}
