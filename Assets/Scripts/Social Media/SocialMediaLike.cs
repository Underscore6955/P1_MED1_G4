using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class SocialMediaLike : PressableObject
{
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Pressed()
    {
        Debug.Log("lildsfgksjd g");
        gameObject.GetComponent<RawImage>().enabled = !gameObject.GetComponent<RawImage>().enabled;
    }

}
