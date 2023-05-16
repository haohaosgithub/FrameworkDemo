using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollowState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = true;
        PlayAnim("Follow");
    }
    public override void Update()
    {
        base.Update();
        if(CanAtkPlayer())
        {
            machine.ChangeState<MonsterAttackState>();
            return;
        }
        if(IsFindPlayer())
        {
            monsterController.navAgent.SetDestination(monsterController.playerController.transform.position);
        }
        else
        {
            machine.ChangeState<MonsterPatrolState>();
        }
    }
}
