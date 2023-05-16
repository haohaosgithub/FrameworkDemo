using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterStateBase : StateBase
{
    public MonsterController monsterController;
    public override void Init(StateMachine machine)
    {
        base.Init(machine);
        monsterController = (MonsterController)machine.owner;
    }

    public void PlayAnim(string animationName)
    {
        monsterController.monsterView.anim.CrossFadeInFixedTime(animationName,0.2f);
    }

    /// <summary>
    /// 发现玩家
    /// </summary>
    /// <returns></returns>
    public bool IsFindPlayer()
    {
        if(Vector3.Distance(monsterController.transform.position,monsterController.playerController.transform.position ) < monsterController.sightRange)
        {
            return true;
        }
        return false;
    }

    public bool CanAtkPlayer()
    {
        if (Vector3.Distance(monsterController.transform.position, monsterController.playerController.transform.position) < monsterController.atkRange)
        {
            return true;
        }
        return false;
    }
}
