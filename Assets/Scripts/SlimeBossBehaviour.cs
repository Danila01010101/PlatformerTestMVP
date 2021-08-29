using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBossBehaviour : MonoBehaviour
{
    
    private enum BossState
    {
        Waiting,
        FirstAttack,
        SecondAttack,
        Dead
    }

    private BossState currentState;

    private void Start()
    {
        
    }

    private void Update()
    {
        switch (currentState)
        {
            case BossState.Waiting:
                UpdateWaitingState();
                break;
            case BossState.FirstAttack:
                UpdateFirstAttackState();
                break;
            case BossState.SecondAttack:
                UpdateSecondAttackState();
                break;
            case BossState.Dead:
                UpdateDeadState();
                break;
        }
    }

    //--WAITING--------------------------------------------------------------------------

    private void EnterWaitingState()
    {

    }

    private void UpdateWaitingState()
    {

    }

    private void ExitWaitingState()
    {

    }

    //--FIRSTATTACK----------------------------------------------------------------------

    private void EnterFirstAttackState()
    {

    }

    private void UpdateFirstAttackState()
    {

    }

    private void ExitFirstAttackState()
    {

    }

    //--SECONDATTACK---------------------------------------------------------------------

    private void EnterSecondAttackState()
    {

    }

    private void UpdateSecondAttackState()
    {

    }

    private void ExitSecondAttackState()
    {

    }

    //--DEAD-----------------------------------------------------------------------------

    private void EnterDeadState()
    {

    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

}
