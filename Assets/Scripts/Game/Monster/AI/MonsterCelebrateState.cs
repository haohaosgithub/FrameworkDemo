using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCelebrateState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Victory");

//        MonoManager.Instance.StartCoroutine(Die());
    }
}
