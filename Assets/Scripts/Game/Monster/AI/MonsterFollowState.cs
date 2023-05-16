using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollowState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = true;
        //monsterController.navAgent.SetDestination(monsterController.playerController.transform.position);
        PlayAnim("Follow");
    }
    public override void Update()
    {
        base.Update();
        if(CanAtkPlayer())
        {

        }
        monsterController.navAgent.SetDestination(monsterController.playerController.transform.position);
    }
}
