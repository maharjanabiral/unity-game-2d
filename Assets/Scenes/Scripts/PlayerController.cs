using System.Collections;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private TrailRenderer _trailRenderer;
    private TouchingDirection _touchingDirection;
    private Vector2 moveInput;
    
    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 0.4f;
    private Vector2 dashDirection;
    private bool _isDashing = false;

    private bool canDash = true;

    private bool _isFacingRight = true;
    private bool _isRunning = false;

    [Header("Jump Settings")]
    [SerializeField] private float jumpImpulse = 3f;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            _animator.SetBool(AnimatorStrings.isRunning, _isRunning);
        }
    }
    
    public bool IsDashing
    {
        get
        {
            return _isDashing;
        }
        private set
        {
            _isDashing = value;
            _animator.SetBool(AnimatorStrings.isDashing, _isDashing);
        }
    }



    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }
            _isFacingRight = value;
        }
    }
    
    public bool CanMove
    {
        get
        {
            return _animator.GetBool(AnimatorStrings.canMove);
        }
    }


    [SerializeField] float speed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _trailRenderer = GetComponent<TrailRenderer>();
        _touchingDirection = GetComponent<TouchingDirection>();
    }

    void Update()
    {
        float fallMultiplier = 2f;
        if (_rb.linearVelocity.y < 0)
        {
            _rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (!CanMove || IsDashing) return;
        _rb.linearVelocity = new Vector2(moveInput.x * speed, _rb.linearVelocity.y);
        _animator.SetFloat(AnimatorStrings.yVelocity, _rb.linearVelocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!CanMove)
        {
            moveInput = Vector2.zero;
            IsRunning = false;
            return;
        }
        moveInput = context.ReadValue<Vector2>();
        IsRunning = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        if (context.started && canDash)
        {
            dashDirection = moveInput;

            if (dashDirection == Vector2.zero)
                dashDirection = IsFacingRight ? Vector2.right : Vector2.left;

            StartCoroutine(Dash());
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    private IEnumerator Dash()
    {
        IsDashing = true;
        canDash = false;
        _trailRenderer.emitting = true;
        float startTime = Time.time;

        // Move fast for dashDuration
        while (Time.time < startTime + dashDuration)
        {
            _rb.linearVelocity = dashDirection * dashSpeed;
            yield return null;
        }

        // End dash
        IsDashing = false;
        _trailRenderer.emitting = false;
        // Cooldown before next dash
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!CanMove) return;
        if (context.started && _touchingDirection.IsGrounded)
        {
            Debug.Log("Jumpo");
            if (_rb.linearVelocity.y < 0)
                _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0);
            _rb.AddForce(Vector2.up * jumpImpulse, ForceMode2D.Impulse);
            _animator.SetTrigger(AnimatorStrings.jump);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log(CanMove);
        if (context.started && CanMove)
        {
            _animator.SetTrigger(AnimatorStrings.attack);
        }
    }
    
    public void TakeDamage(int Damage)
    {
        
    }

}
