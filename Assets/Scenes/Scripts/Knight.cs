using UnityEngine;

public class Knight : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private TouchingDirection _touchingDirection;
    private Damageable _playerDamageable;

    [SerializeField] private DetectCollision attackZone;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float distance = 5f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float damageAmount = 10f;

    private GameObject _player;
    private bool _hasTarget = false;
    private bool _isPlayerClose = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimatorStrings.hasTarget, _hasTarget);
        }
    }

    public bool IsPlayerClose
    {
        get { return _isPlayerClose; }
        private set
        {
            _isPlayerClose = value;
            _animator.SetBool(AnimatorStrings.isPlayerClose, _isPlayerClose);
        }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirection = GetComponent<TouchingDirection>();
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player != null)
        {
            _playerDamageable = _player.GetComponent<Damageable>();
        }
    }

    void Update()
    {
        if (_player == null || _playerDamageable == null || !_playerDamageable.IsAlive)
        {
            IsPlayerClose = false;
            HasTarget = false;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            IsPlayerClose = true;
            Attack();
        }
        else if (distanceToPlayer < distance)
        {
            IsPlayerClose = true;
            HasTarget = false;
            Vector2 direction = (_player.transform.position - transform.position).normalized;
            _rb.linearVelocity = new Vector2(direction.x * moveSpeed, _rb.linearVelocity.y);
        }
        else
        {
            IsPlayerClose = false;
            HasTarget = false;
            _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        }
    }

    private void Attack()
    {
        _rb.linearVelocity = new Vector2(0, _rb.linearVelocity.y);
        HasTarget = true;
    }


    public void OnHit(int damage, Vector2 knockback)
    {
        _rb.linearVelocity = new Vector2(knockback.x, _rb.linearVelocity.y + knockback.y);
    }
}