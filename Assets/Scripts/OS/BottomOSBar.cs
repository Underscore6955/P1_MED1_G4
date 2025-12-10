using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// script that handels the bottom bar, like on the operating system with the apps you get it
public class BottomOSBar : MonoBehaviour
{
    // singleton i think maybe dont think about it 100 emoji
    [SerializeField] RectTransform canvasAssign;
    static RectTransform canvasObj;
    // dictionary to keep track of the GameObject, which is the app and the GameObject which is the shortcut on the bottom bar
    public static Dictionary<GameObject,GameObject> apps = new Dictionary<GameObject,GameObject>();
    private void Start()
    {
        // more singleton stuff dont worry bout it
        canvasObj = canvasAssign;
        // size the thing according to the screen size, so it always fits on the screen nicely
        canvasObj.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }
    // name is pretty clear
    public static void AddToBar(GameObject app, Texture2D icon)
    {
        // create a new gameobject and add the stuff to it
        GameObject newApp = new GameObject("AAA");
        newApp.transform.SetParent(canvasObj);
        RectTransform rt = newApp.AddComponent<RectTransform>();
        newApp.AddComponent<RawImage>();
        // apply the image, this dosen't work rn for whatever reason ill have to look into it
        newApp.transform.localScale = OSMechanics.ResizeImageToSize(icon,rt,25f);
        // make the collider and size it
        newApp.AddComponent<BoxCollider2D>().size = rt.sizeDelta;
        // add the app shortcut script, and give it the correct variables
        AppShortcut AS = newApp.AddComponent<AppShortcut>();
        AS.opened = true;
        AS.bc = newApp.GetComponent<BoxCollider2D>();
        AS.app = app;
        // add this shortcut to the dictionary
        apps.Add(app,newApp);
        // place the shortcut correctly
        rt.position = Camera.main.ScreenToWorldPoint(Vector3.one*15) +  0.5f*Vector3.right*(apps.Keys.ToList().IndexOf(app));
        ;
    }
}
