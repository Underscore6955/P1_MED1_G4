using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButton : PressableObject
{
    public string sceneName; //clicking button leads to scene (write scene-name in unity inspector)
    public Sprite clickSprite; //Sprite of button being clicked on (assign in unity inspector)

    public Sprite overSprite; //mouse over sprite
    private Sprite originalSprite;

    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
    }

    public void OnMouseEnter()
    {
        if (overSprite != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = overSprite;
        }
    }
    public void OnMouseExit()
    {
        if (overSprite != null)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = originalSprite;
        }
    }

    public override void Pressed()
    {
        if (clickSprite != null) //Changes to "clicked" sprite if one is assigned
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = clickSprite;
        }
    }

    public override void Release() 
    {
        if (clickSprite != null) //Changes back to og. sprite
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = originalSprite;
        }

        SceneManager.LoadScene(sceneName); //Scene change
    }
}