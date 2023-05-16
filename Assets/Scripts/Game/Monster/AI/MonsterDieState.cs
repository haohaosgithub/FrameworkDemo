using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework;
public class MonsterDieState : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.navAgent.enabled = false;
        PlayAnim("Die");
        MonoManager.Instance.StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        yield return new WaitForSeconds(3);
        EventManager.Instance.EventTrigger("MonsterDie");
        monsterController.Die();
    }
}
