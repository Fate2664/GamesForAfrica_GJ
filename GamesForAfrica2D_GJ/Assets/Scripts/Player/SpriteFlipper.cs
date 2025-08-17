using UnityEngine;

public class SpriteFlipper : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private float _previousXPosition;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _previousXPosition = transform.position.x;
    }

    private void Update()
    {
        float currentX = transform.position.x;

        if (Mathf.Abs(currentX - _previousXPosition) > 0.001f) 
        {
            bool movingRight = currentX > _previousXPosition;
            bool movingLeft = currentX < _previousXPosition;

            if (movingRight && _spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = false;
            }
            else if (movingLeft && !_spriteRenderer.flipX)
            {
                _spriteRenderer.flipX = true;
            }
        }

        _previousXPosition = currentX;
    }
}