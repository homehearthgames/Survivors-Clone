using UnityEngine;

public class FadeOutAndDestroy : MonoBehaviour
{
    public float fadeDuration = 1.0f;

    private SpriteRenderer spriteRenderer;
    private float fadeTimer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        fadeTimer = fadeDuration;
    }

    private void Update()
    {
        fadeTimer -= Time.deltaTime;

        if (fadeTimer <= 0)
        {
            Destroy(gameObject);
            return;
        }

        float alpha = fadeTimer / fadeDuration;
        Color newColor = spriteRenderer.color;
        newColor.a = alpha;
        spriteRenderer.color = newColor;
    }
}
