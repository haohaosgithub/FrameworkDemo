using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterStateBase
{
    Coroutine patrolCoroutine;
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Idle");

        patrolCoroutine = MonoManager.Instance.StartCoroutine(Patrol());
    }

    public override void Update()
    {
        base.Update();
        if(IsFindPlayer())
        {
            if (patrolCoroutine != null)
            {
                MonoManager.Instance.StopCoroutine(patrolCoroutine);
            }
            machine.ChangeState<MonsterFollowState>();
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        if (patrolCoroutine != null)
        {
            MonoManager.Instance.StopCoroutine(patrolCoroutine);
        }
    }
    public IEnumerator Patrol()
    {
        yield return new WaitForSeconds(monsterController.waitNextPatrolTime);
       
        machine.ChangeState<MonsterPatrolState>();
    }

    
}
