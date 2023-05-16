using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Attack");
    }

    public override void Update()
    {
        base.Update();
        if(!CanAtkPlayer())
        {
            if(IsFindPlayer())
            {
                machine.ChangeState<MonsterFollowState>();
                return;
            }

        }

    }

}
