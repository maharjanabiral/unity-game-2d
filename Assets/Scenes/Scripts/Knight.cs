using UnityEditor.Tilemaps;
using UnityEngine;

public class Knight : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private TouchingDirection _touchingDirection;
    [SerializeField] private DetectCollision attackZone;
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float damageAmount = 10f;
    private float enemyRange = 7.0f;
    private GameObject _player;
    private bool _hasTarget = false;

    private bool _isRunning = false;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkableDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkableDirection;
        }
        private set
        {
            if(_walkableDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkableDirection = value;
        }
    }

    public bool HasTarget {
        get
        {
            return _hasTarget;
        } private set
        {
            _hasTarget = value;
            _animator.SetBool(AnimatorStrings.hasTarget, _hasTarget);
        } 
    }

    // public bool IsRunning
    // {
    //     get
    //     {
    //         return _isRunning;
    //     }
    //     private set
    //     {
    //         _isRunning = value;
    //         _animator.SetBool(AnimatorStrings.isRunning, value); 
    //     }
    // }

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _touchingDirection = GetComponent<TouchingDirection>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void FixedUpdate()
    {

        // if (_touchingDirection.IsGrounded && _touchingDirection.IsOnWall)
        // {
        //     FlipDirection();
        // }
        // float targetSpeed = walkSpeed + walkDirectionVector.x;
        // _rb.linearVelocity = new Vector2(targetSpeed, _rb.linearVelocity.y);
    }
    
    // Update is called once per frame
    void Update()
    {
        // HasTarget = attackZone.detectedColliders.Count > 0;
        // float distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);

        // if (_touchingDirection.IsGrounded && HasTarget)
        // {
        //     if (distanceToPlayer < enemyRange)
        //     {
        //         Vector2 direction = (_player.transform.position - transform.position).normalized;
        //         _rb.MovePosition(_rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        //         return;
        //     }
        // }

    }

    // private void FlipDirection()
    // {
    //     if (WalkDirection == WalkableDirection.Right)
    //     {
    //         WalkDirection = WalkableDirection.Left;
    //     }else if(WalkDirection == WalkableDirection.Left)
    //     {
    //         WalkDirection = WalkableDirection.Right;
    //     }
    // }


}
