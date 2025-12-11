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
    [SerializeField] GameObject shortcutPrefabAssign;

    static RectTransform canvasObj;
    static GameObject shortcutPrefab;
    // dictionary to keep track of the GameObject, which is the app and the GameObject which is the shortcut on the bottom bar
    public static Dictionary<GameObject,GameObject> apps = new Dictionary<GameObject,GameObject>();
    private void Start()
    {
        // more singleton stuff dont worry bout it
        canvasObj = canvasAssign;
        shortcutPrefab = shortcutPrefabAssign;
        // size the thing according to the screen size, so it always fits on the screen nicely
        canvasObj.sizeDelta = new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight);
    }
    // name is pretty clear
    public static void AddToBar(GameObject app, Texture2D icon)
    {
        // create a new gameobject and add the stuff to it
        GameObject newApp = Instantiate(shortcutPrefab, canvasObj);
        RectTransform rt = newApp.GetComponent<RectTransform>();
        newApp.transform.localScale = OSMechanics.ResizeImageToSize(icon,rt,25f);
        // add the app shortcut script, and give it the correct variables
        AppShortcut AS = newApp.GetComponent<AppShortcut>();
        AS.opened = true;
        AS.bc = newApp.GetComponent<BoxCollider2D>();
        AS.app = app;
        // add this shortcut to the dictionary
        apps.Add(app,newApp);
        // place the shortcut correctly
        rt.position = Camera.main.ScreenToWorldPoint(Vector3.zero) + Vector3.up*0.22f +0.5f*Vector3.right*(apps.Keys.ToList().IndexOf(app))+ Vector3.right*0.22f;
        rt.localPosition = new Vector3(rt.localPosition.x,rt.localPosition.y,0);
    }
}
