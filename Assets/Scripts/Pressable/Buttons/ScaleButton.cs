using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScaleButton : PressableObject
{
    public int value;
    public TMP_Text text;
    public bool disc;
    public ScaleScript SC;
    public override void Release()
    {
        StartCoroutine(Answered());
    }
    IEnumerator Answered()
    {
        foreach (GameObject g in SC.buttons) { g.transform.position += Vector3.left * 999f; }
        yield return StartCoroutine(SC.chat.SMS.SendText((value.ToString(),1,null)));
        if (disc) { ScaleScript.disc += value; } else { ScaleScript.stand += value; }
        SC.choosing = false;
    }
}
