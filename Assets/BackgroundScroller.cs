using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 1f;
    private Vector3 startPosition;
    private float width;

    void Start()
    {
        startPosition = transform.position;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            width = spriteRenderer.bounds.size.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, width);
        transform.position = startPosition + Vector3.left * newPosition;

        if (transform.position.x <= startPosition.x - width)
        {
            transform.position = startPosition;
        }
    }
}
