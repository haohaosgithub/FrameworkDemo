using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour ,IStateMachineOwner
{
    private StateMachine stateMachine;
    public NavMeshAgent navAgent;
    public MonsterView monsterView;

    #region 怪物AI相关配置
    public float sightRange;     
    public float stopPatrolRange; 
    public float atkRange;  
    public float waitNextPatrolTime;
    #endregion

    int curHP;

    public PlayerController playerController;
    public void Init(MonsterConfig monsterConfig)
    {
        #region 怪物AI相关配置赋值
        sightRange = monsterConfig.sightRange;
        stopPatrolRange = monsterConfig.stopPatrolRange;
        atkRange = monsterConfig.atkRange;
        waitNextPatrolTime = monsterConfig.waitNextPatrolTime;
        #endregion

        curHP = monsterConfig.hp;
        playerController = PlayerController.Instance;
        
        monsterView = PoolManager.Instance.GetGameObject<MonsterView>(monsterConfig.prefab,transform);
        monsterView.transform.localPosition = Vector3.zero;
        monsterView.Init();
        monsterView.endGetHitAction += EndGetHit;

        navAgent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        stateMachine.ChangeState<MonsterIdleState>();
    }
    /// <summary>
    /// 受伤逻辑
    /// </summary>
    public void GetHit(int dmg)
    {
        curHP -= dmg;
        if (curHP <= 0)
        {
            stateMachine.ChangeState<MonsterDieState>();
        }
        else
        {
            stateMachine.ChangeState<MonsterGetHitState>();
        }
    }

    public void EndGetHit()
    {
        stateMachine.ChangeState<MonsterFollowState>();
    }

    public Vector3 GetPatrolPoint()
    {
        return MonsterManager.Instance.GetPatrolPoint();
    }
}
