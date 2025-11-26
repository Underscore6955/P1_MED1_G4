using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BottomOSBar : MonoBehaviour
{
    [SerializeField] Transform canvasAssign;
    static Transform canvasObj;
    public static Dictionary<GameObject,GameObject> apps = new Dictionary<GameObject,GameObject>();
    private void Start()
    {
        canvasObj = canvasAssign;
    }
    public static void AddToBar(GameObject app, Texture2D icon)
    {
        GameObject newApp = Instantiate(new GameObject(), canvasObj);
        RectTransform rt = newApp.AddComponent<RectTransform>();
        newApp.AddComponent<RawImage>();
        newApp.transform.localScale = OSMechanics.ResizeImageToSize(icon,rt,20f);
        newApp.AddComponent<BoxCollider2D>().size = rt.sizeDelta;
        AppShortcut AS = newApp.AddComponent<AppShortcut>();
        AS.opened = true;
        AS.bc = newApp.GetComponent<BoxCollider2D>();
        AS.app = app;
        apps.Add(app,newApp);
        rt.position = Camera.main.ScreenToWorldPoint(Vector3.one*30) + Vector3.right*apps.Keys.ToList().IndexOf(app);
        ;
    }
}
