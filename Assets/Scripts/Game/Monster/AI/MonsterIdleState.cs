using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Idle");
        
        MonoManager.Instance.StartCoroutine(Patrol());
    }

    public IEnumerator Patrol()
    {
        yield return new WaitForSeconds(1);
       
        machine.ChangeState<MonsterPatrolState>();
    }
}
