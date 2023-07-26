using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float speed = 1f; // Speed at which the text floats upwards
    public float fadeTime = 1f; // Time it takes for the text to fade out
    public float destroyDelay = 0.5f; // Delay before destroying the object after fading out
    public float shrinkRate = 0.1f; // Rate at which the text shrinks over time

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        StartCoroutine(FadeAndRise());
    }

    private IEnumerator FadeAndRise()
    {
        float t = 0;
        Color originalColor = textMesh.color;
        Vector3 originalPosition = transform.position;
        float originalFontSize = textMesh.fontSize;

        while (t < fadeTime)
        {
            // Interpolate color, position, and font size over time
            t += Time.deltaTime;
            float normalizedTime = t / fadeTime; // Calculates fraction of the way through the fade

            // Fade alpha to 0
            textMesh.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0, normalizedTime));

            // Rise upwards
            transform.position = originalPosition + new Vector3(0, speed * normalizedTime, 0);

            // Shrink the font size
            float newSize = Mathf.Lerp(originalFontSize, 0, normalizedTime);
            textMesh.fontSize = newSize;

            yield return null; // Wait until the next frame
        }

        Destroy(gameObject.transform.parent.gameObject, destroyDelay);
    }
}
