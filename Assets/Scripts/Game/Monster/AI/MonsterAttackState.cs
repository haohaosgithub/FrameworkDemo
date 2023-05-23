using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MonsterAttackState : MonsterStateBase
{
    public override void Enter()
    {
        //Debug.Log("enter atk");
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Attack");
    }

    //实际由动画事件（EndAttack）驱动
    //public override void Update()
    //{
    //    base.Update();
    //    if(!CanAtkPlayer())
    //    {
    //        if(IsFindPlayer())
    //        {
    //            machine.ChangeState<MonsterFollowState>();
    //            return;
    //        }

    //    }

    //}

}
