using UnityEngine;

public class Attack : MonoBehaviour
{

    public int damageAmount = 10;
    public Vector2 knockback;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if (damageable != null)
        {
            bool gotHit = damageable.Hit(damageAmount, knockback);
            if (gotHit)
                Debug.Log(collision.name + "hit for " + damageAmount);
        }
    }

}
