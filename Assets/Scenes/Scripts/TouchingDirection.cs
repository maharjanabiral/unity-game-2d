using TMPro;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    private CapsuleCollider2D _col;
    private Rigidbody2D _rb;
    private Animator _animator;
    [SerializeField] private ContactFilter2D castFilter;
    private RaycastHit2D[] groundHits = new RaycastHit2D[5];
    private RaycastHit2D[] wallHits = new RaycastHit2D[5];

    private RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    private float groundDistance = 0.05f;
    private float wallDistance = 0.5f;

    private float ceilingDistance = 0.2f;


    [SerializeField]
    private bool _isGrounded = true;

    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            _animator.SetBool(AnimatorStrings.isGrounded, value);
        }
    }

    private bool _isOnWall = false;
    public bool IsOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            {
                _isOnWall = value;
                _animator.SetBool(AnimatorStrings.isOnWall, value);
            }
        }
    }

    private bool _isOnCeiling = false;
    public bool IsOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            _animator.SetBool(AnimatorStrings.isOnCeiling, value);
        }
    }

    private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _col = GetComponent<CapsuleCollider2D>();
    }

    void FixedUpdate()
    {
        IsGrounded = _col.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
        IsOnWall = _col.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        IsOnCeiling = _col.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;

    }
}
