using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : PressableObject
{
    public string SceneName; //clicking button leads to scene (write scene-name in unity inspector)
    public Sprite ClickSprite; //Sprite of button being clicked on (assign in unity inspector)
    private Sprite OriginalSprite;

    public override void Pressed()
    {
        if (ClickSprite != null) //Changes to "clicked" sprite if one is assigned
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            OriginalSprite = spriteRenderer.sprite;
            spriteRenderer.sprite = ClickSprite;
        }
    }

    public override void Release() 
    {
        if (ClickSprite != null) //Changes back to og. sprite
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = OriginalSprite;
        }

        SceneManager.LoadScene(SceneName); //Scene change
    }
}