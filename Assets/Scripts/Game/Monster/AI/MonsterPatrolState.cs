using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrolState : MonsterStateBase
{
    Vector3 nowTarget;
    public override void Enter()
    {
        
        base.Enter();
        
        nowTarget = monsterController.GetPatrolPoint();
        
        monsterController.navAgent.enabled = true ;
        monsterController.navAgent.SetDestination(nowTarget);
        PlayAnim("Patrol");
    }

    public override void Update() 
    {
        base.Update();
        if (IsFindPlayer())
        {
            machine.ChangeState<MonsterFollowState>();
            return;
        }
        if (Vector3.Distance(monsterController.transform.position, nowTarget) < monsterController.stopPatrolRange)
        {
            machine.ChangeState<MonsterIdleState>();
        }
    }
}
