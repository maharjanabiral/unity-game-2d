using UnityEngine;

public class Damageable: MonoBehaviour
{
    private Animator _animator;
    private int _maxHealth = 100;
    
    [SerializeField]
    private bool isInvincible = false;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        private set
        {
            _maxHealth = value;
        }
    }

    private int _health = 100;
    public int Health
    {
        get
        {
            return _health;
        }
        private set
        {
            _health = value;
        }
    }

    private bool _isAlive = true;
    private float timeSinceHit = 0f;
    public float invincibilityTime = 0.2f;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            _animator.SetBool(AnimatorStrings.isAlive, value);
            Debug.Log("Is Alive:" + value);
        }
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Update() {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

    }


    public void Hit(int damage)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Health = 0;
                IsAlive = false;
            }
            isInvincible = true;    
        }
    }

}
