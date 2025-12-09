using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{

    Vector3 movePosition = new Vector3(0, 75, 0);
    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;
    public float timeToFade = 0.5f;
    private float elapsedTime = 0f;
    private Color startColor;

    void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    void Update()
    {
        textTransform.position += movePosition * Time.deltaTime;
        elapsedTime += Time.deltaTime;

        if(elapsedTime < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (elapsedTime / timeToFade));
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
