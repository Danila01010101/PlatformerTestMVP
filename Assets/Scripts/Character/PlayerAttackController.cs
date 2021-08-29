using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    [SerializeField]
    private float inputTimer,
        attack1Radius,
        attack1Damage;

    private Movement _movement;

    [SerializeField]
    private bool combatEnabled;

    [SerializeField]
    private Transform attackHitBoxPos;
    [SerializeField]
    private LayerMask whatIsDamageble;

    private bool gotInput,
        isAttacking,
        isFirstAttack;

    private float lastInputTime = Mathf.NegativeInfinity;

    private float[] attackDetails = new float[2];

    private Animator _anim;

    public bool TouchInput = false;

    [SerializeField]
    private Vector2 knockbackSpeed;

    private PlayerStats PS;

    private void Start()
    {
        _anim = GetComponent<Animator>();

        _anim.SetBool("CanAttack", combatEnabled);  

        _movement = GetComponent<Movement>();

        PS = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    public void CheckCombatInput()
    {
        if (TouchInput)
        {
            if (combatEnabled)
            {
                //Attempt combat
                gotInput = true;
                lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (gotInput)
        {
            //perform Attack1
            if (!isAttacking)
            {
                gotInput = false;
                isAttacking = true;
                isFirstAttack = !isFirstAttack;
                _anim.SetBool("attack1",true);
                _anim.SetBool("firstAttack", isFirstAttack);
                _anim.SetBool("isAttacking", isAttacking);
            }
        }

        if (Time.time <= lastInputTime + inputTimer)
        {
            gotInput = false;
        }
    }
    private void CheckAttackHitbox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackHitBoxPos.position, attack1Radius, whatIsDamageble);

        foreach(Collider2D collider in detectedObjects)
        {
            attackDetails[0] = attack1Damage;
            attackDetails[1] = transform.position.x;
            collider.transform.parent.SendMessage("Damage", attackDetails);
            //instantiate hit particle
        }
    }
    
    private void FinishAttack1()
    {
        isAttacking = false;
        _anim.SetBool("isAttacking", isAttacking);
        _anim.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackHitBoxPos.position, attack1Radius);
    }

    public void OnPointerDown()
    {
        TouchInput = true;
    }

    public void OnPointerUp()
    {
        TouchInput = false;
    }

    private void Damage(float[] attackDetails)
    {
        //Damage here
        int direction;

        PS.DecreaseHealth(attackDetails[0]);

        if (attackDetails[1] < transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
        _movement.KnockBack(direction);
    }
}
