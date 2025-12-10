using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class Damageable: MonoBehaviour
{
    private Animator _animator;
    [SerializeField]
    private HealthBar healthBar;
    [SerializeField]
    private UIManager uiManager;
    private int _maxHealth = 100;
    public UnityEvent<int, Vector2> damageableHit;
    
    [SerializeField]
    private bool isInvincible = false;
    private CinemachineImpulseSource source;
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

    [SerializeField]
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
            if(_health <= 0)
            {
                IsAlive = false;
                if (uiManager != null)
                    uiManager.GameOver();
            }
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
        }
    }

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        source = GetComponentInParent<CinemachineImpulseSource>();
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(MaxHealth);
            healthBar.SetHealth(Health);
        }
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


    public bool Hit(int damage, Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            if (healthBar != null)
                healthBar.SetHealth(Health);

            isInvincible = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            Debug.Log("Source = " + source);
            Debug.Log("CameraShakeManager.instance = " + CameraShakeManager.instance);

            CameraShakeManager.instance.CameraShake(source);
            return true;
        }
        return false;
    }

    public void Heal(int healthRestored)
    {
        if (IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestored);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            if (healthBar != null)
                healthBar.SetHealth(Health);
        }
    }

}
