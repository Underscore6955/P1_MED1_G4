using UnityEngine;
using TMPro;
using System.Collections;

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
        yield return StartCoroutine(SC.chat.SMS.SendText((value.ToString(),1,null)));
        if (disc) { SC.disc += value; } else { SC.stand += value; }
        SC.choosing = false;
    }
}
