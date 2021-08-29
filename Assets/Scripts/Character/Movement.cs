using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    Rigidbody2D _rb;
    public Joystick _Joystick;
    private float _MovementInput,
        knockbackStartTime;

    [SerializeField]
    private float knockbackDuration;

    [SerializeField]
    private Vector2 knockbackSpeed;

    public float _movementSpeed = 1f;
    private bool _isFacingRight = true;
    public float _jumpforce = 10f;
    private float _jumpInput;
    private Animator _animator;
    private bool IsWalking;
    public Transform groundCheck;
    public bool 
        isGrounded,
        canFlip,
        knockback;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator> ();
    }

    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckKnockback();
    }
    
    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    private void CheckInput() 
    {
        _MovementInput = _Joystick.Horizontal;

        _jumpInput = _Joystick.Vertical;
    }

    private void ApplyMovement()
    {
        if (!knockback)
        {
            _rb.velocity = new Vector2(_movementSpeed * _MovementInput, _rb.velocity.y);
        }
    }

    private void CheckMovementDirection()
    {
        if (_isFacingRight && _MovementInput < 0)
        {
            Flip();
        }
        else if (!_isFacingRight && _MovementInput > 0)
        {
            Flip();
        }

        if (Mathf.Abs(_rb.velocity.x) >= 0.01f)
        {
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
        }
    }

    private void Flip()
    {
        if (!knockback)
        {
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    public void DisableFlip()
    {
        canFlip = false;
    }

    public void EnableFlip()
    {
        canFlip = true;
    }

    public void Jump()
    {
        if (isGrounded && !knockback)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpforce);
        }
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("IsWalking", IsWalking);
        _animator.SetBool("IsGrounded", isGrounded);
        _animator.SetFloat("yVelocity", _rb.velocity.y);
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    public void KnockBack(int direction)
    {
        knockback = true;
        knockbackStartTime = Time.time;
        _rb.velocity = new Vector2(knockbackSpeed.x * direction, knockbackSpeed.y);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStartTime + knockbackDuration && knockback)
        {
            knockback = false;
            _rb.velocity = new Vector2(0.0f, _rb.velocity.y);
        }
    }


}
