using System.Collections;
using UnityEngine;

public class PlayerControllerOld : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _animator;
    private TrailRenderer _trailRenderer;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckRadius = 0.15f;
    [SerializeField] private float _dashVelocity = 14f;
    [SerializeField] private float _dashDuration = 0.5f;

    private bool _isGrounded;
    private bool _isDashing;
    private bool _canDash = true;
    private Vector2 _dashDirection;


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    void FixedUpdate()
    {
        if(_isDashing) return;
        float horizontalInput = Input.GetAxis("Horizontal");

        _rb.linearVelocity = new Vector2(horizontalInput * _speed, _rb.linearVelocity.y);

        Flip(horizontalInput);
    

       
       
        _animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

    void Update()
    {
        if (_isDashing) return;
        _isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _groundLayer);
        var dashInput = Input.GetKeyDown(KeyCode.E);

        if(_isGrounded)
        {
            _canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
         if(dashInput && _canDash)
        {
            Debug.Log("Dash");
            _isDashing = true;
            _canDash = false;
            _trailRenderer.emitting = true;
            _dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (_dashDirection == Vector2.zero)
            {
                _dashDirection = new Vector2(transform.localScale.x, 0);
            }
            _animator.SetBool("Dash", true);
            StartCoroutine(DashCooldown());
        }
        if(_isDashing)
        {
            _rb.linearVelocity = _dashDirection.normalized * _dashVelocity;
            return;
        }
        
    }

    private void Flip(float horizontalInput)
    {
        if (Mathf.Abs(horizontalInput) < 0.1f) return; 

        Vector3 scale = transform.localScale;

        if (horizontalInput > 0.1f)
            scale.x = Mathf.Abs(scale.x);
        else if (horizontalInput < -0.1f)
            scale.x = -Mathf.Abs(scale.x);

        transform.localScale = scale;
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(_dashDuration);
        _isDashing = false;
        _trailRenderer.emitting = false;
        _animator.SetBool("Dash", false);

        yield return new WaitForSeconds(0.2f);
        _canDash = true;
    }
}
