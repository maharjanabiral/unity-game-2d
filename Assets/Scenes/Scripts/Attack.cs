using UnityEngine;

public class Attack : MonoBehaviour
{

    public int damageAmount = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.Hit(damageAmount);
            Debug.Log(collision.name + "hit for " + damageAmount);
        }
    }
    
}
