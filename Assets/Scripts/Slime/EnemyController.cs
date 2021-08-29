using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private enum State
    {
        Walking, 
        Knockback,
        Dead
    }

    private State currentState;

    [SerializeField]
    private Transform
        groundCheck,
        wallCheck;
    [SerializeField]
    private Transform touchDamageCheck;
    [SerializeField]
    private LayerMask whatIsGround,
        whatIsPlayer;

    [SerializeField]
    private Vector2 KnockBackSpeed;

    [SerializeField]
    private GameObject deathChunkParticle;

    private float
        CurrentHealth,
        KnockBackStartTime;

    private float[] attackDetails = new float[2];

    [SerializeField]
    private float
        MaxHealth,
        groundCheckDistance,
        movementSpeed,
        wallCheckDistance,
        KnockBackDuration,
        lastTouchDamageTime,
        TouchDamageCooldown,
        touchDamage,
        touchDamageWidth,
        touchDamageHight;

    private int 
        facingDirection,
        damageDirection;

    private Vector2 movement,
            touchDamageBotLeft,
            touchDamageTopRight;

    private bool
        groundDetected,
        wallDetected;

    private GameObject Alive;

    private Rigidbody2D AliveRb;

    private Animator aliveAnim;

    public GameObject
        hitParticle,
        deathChunk,
        deathSlimeParticle;

    private void Start()
    {
        Alive = transform.Find("Alive").gameObject;
        AliveRb = Alive.GetComponent<Rigidbody2D>();
        aliveAnim = Alive.GetComponent<Animator>();

        facingDirection = 1;
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    //--Walking state---------------------------------------------------------------------------

    private void EnterWalkingState()
    {

    }

    private void UpdateWalkingState()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsGround);

        CheckTouchDamage();

        if (!groundDetected || wallDetected)
        {
            Flip();
        }
        else
        {
            movement.Set(movementSpeed * facingDirection, AliveRb.velocity.y);
            AliveRb.velocity = movement;
        }
    }

    private void ExitWalkingState()
    {

    }

    //--KnockbackState---------------------------------------------------------------------------

    private void EnterKnockbackState()
    {
        KnockBackStartTime = Time.time;
        movement.Set(KnockBackSpeed.x * damageDirection, KnockBackSpeed.y);
        AliveRb.velocity = movement;

        aliveAnim.SetBool("KnockBack", true);
    }

    private void UpdateKnockbackState()
    {
        if (Time.time >= KnockBackStartTime + KnockBackDuration)
        {
            SwitchState(State.Walking);
        }
    }

    private void ExitKnockbackState()
    {
        aliveAnim.SetBool("KnockBack", false);
    }

    //--DeadState---------------------------------------------------------------------------

    private void EnterDeadState()
    {
        Instantiate(deathChunkParticle, Alive.transform.position, deathChunkParticle.transform.rotation);
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    //--OtherFunctions------------------------------------------------------------------------

    private void CheckTouchDamage()
    {
        if (Time.time >= lastTouchDamageTime + TouchDamageCooldown)
        {
            touchDamageBotLeft.Set(touchDamageCheck.position.x - (touchDamageWidth/2), touchDamageCheck.position.y - (touchDamageHight/2));
            touchDamageTopRight.Set(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHight / 2));

            Collider2D hit = Physics2D.OverlapArea(touchDamageBotLeft, touchDamageTopRight, whatIsPlayer);

            if ( hit != null)
            {
                lastTouchDamageTime = Time.time;
                attackDetails[0] = touchDamage;
                attackDetails[1] = Alive.transform.position.x;
                hit.SendMessage("Damage", attackDetails);
            }
        }
    }

    private void Damage(float[] AttackDetails)
    {
        CurrentHealth -= AttackDetails[0];

        Instantiate(hitParticle, Alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (AttackDetails[1] > Alive.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //Hit Particle-----------------------------------------------------

        if (CurrentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if (CurrentHealth < 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void Flip()
    {
        facingDirection *= -1;
        Alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }
    private void SwitchState(State state)
    {
        switch (currentState)
        {
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

        Vector2 bottomLeft = new Vector2(touchDamageCheck.position.x -(touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHight / 2));
        Vector2 bottomRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y - (touchDamageHight / 2));
        Vector2 topRight = new Vector2(touchDamageCheck.position.x + (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHight / 2));
        Vector2 topLeft = new Vector2(touchDamageCheck.position.x - (touchDamageWidth / 2), touchDamageCheck.position.y + (touchDamageHight / 2));

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft,bottomLeft);
    }
}
