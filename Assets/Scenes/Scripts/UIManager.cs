using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas canvas;

    private void Awake()
    {
        canvas = FindAnyObjectByType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += CharacterTookDamage;
        CharacterEvents.characterHealed += CharacterHealed;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= CharacterTookDamage;
        CharacterEvents.characterHealed -= CharacterHealed;
    }

    private void CharacterTookDamage(GameObject character, int damageReceived)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    private void CharacterHealed(GameObject character, int healthRestored)
    {
        Vector3 spawnPos = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPos, Quaternion.identity, canvas.transform).GetComponent<TMP_Text>();
        tmpText.text = healthRestored.ToString();
    }
    
}
