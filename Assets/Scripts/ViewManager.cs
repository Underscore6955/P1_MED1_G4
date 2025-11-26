using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ViewManager : MonoBehaviour
{
    static List<GameObject> sortOrder = new List<GameObject>();

    public static void AddToOrder(GameObject obj)
    {
        sortOrder.Remove(obj);
        sortOrder.Add(obj);
        UpdateOrder();
    }
    static void UpdateOrder()
    {
        for (int i = sortOrder.Count-1; i >= 0; i--)
        {
            GameObject obj = sortOrder[i];
            obj.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,90-i);
            obj.GetComponent<Canvas>().sortingOrder = 90 + i;
        }
    }
}
