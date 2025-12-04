using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ViewManager : MonoBehaviour
{
    // ordered list to keep track of where things need to be on the z axis
    static List<GameObject> sortOrder = new List<GameObject>();

    public static void AddToOrder(GameObject obj)
    {
        // we remove the obj from the list and add it again, meaning we place it at the top of the list
        sortOrder.Remove(obj);
        sortOrder.Add(obj);
        // update all objects in the list, whenever a change happens
        UpdateOrder();
    }
    static void UpdateOrder()
    {
        // go through each object
        for (int i = sortOrder.Count-1; i >= 0; i--)
        {
            GameObject obj = sortOrder[i];
            // place the gameobject at the right z coodinate, this is to make raycasts hit properly
            obj.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,90-i);
            // set the canvas sorting order, this is so it looks right to the player
            obj.GetComponent<Canvas>().sortingOrder = 90 + i;
        }
    }
}
