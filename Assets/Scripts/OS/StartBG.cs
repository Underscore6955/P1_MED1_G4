using UnityEngine;
using UnityEngine.UI;

//og code from Tarodev on yt. Help from budhhai too. 
public class StartBG : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Vector2 _movementSpeed;

    void Update()
    {
        Vector3 newPosition = _spriteRenderer.transform.position + new Vector3(_movementSpeed.x, _movementSpeed.y, 0f) * Time.deltaTime;

        Bounds spriteBounds = _spriteRenderer.bounds;

        // Wrap and repeat the sprite horizontally
        if (Mathf.Abs(newPosition.x - transform.position.x) > spriteBounds.size.x / 2)
        {
            float offsetX = (newPosition.x - transform.position.x) % spriteBounds.size.x;
            newPosition.x = transform.position.x + offsetX;
        }

        // Wrap and repeat the sprite vertically
        if (Mathf.Abs(newPosition.y - transform.position.y) > spriteBounds.size.y / 2)
        {
            float offsetY = (newPosition.y - transform.position.y) % spriteBounds.size.y;
            newPosition.y = transform.position.y + offsetY;
        }

        _spriteRenderer.transform.position = newPosition;
    }
}
