using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGetHitState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("GetHit");
    }
}
