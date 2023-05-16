using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Die");
    }
}
