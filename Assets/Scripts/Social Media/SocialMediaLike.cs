using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediaLike : PressableObject
{
    
    public override void Pressed()
    {
        gameObject.GetComponent<RawImage>().enabled = !gameObject.GetComponent<RawImage>().enabled;
    }

}
