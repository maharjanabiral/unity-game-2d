using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private int healthRestored = 10;
    void Start()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hello");
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            damageable.Heal(healthRestored);
            Destroy(gameObject);
        }
    }
}
