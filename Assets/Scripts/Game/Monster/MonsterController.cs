using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Pool]
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
    public int atk;
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
        atk = monsterConfig.atk;
        #endregion

        curHP = monsterConfig.hp;
        playerController = PlayerController.Instance;
        
        //monsterView = PoolManager.Instance.GetGameObject<MonsterView>(monsterConfig.prefab,transform);
        monsterView = ResManager.Instance.Load<MonsterView>(monsterConfig.prefabStr, transform);
        monsterView.transform.localPosition = Vector3.zero;
        monsterView.Init();
        monsterView.EndGetHitAction += EndGetHit; //受击结束
        monsterView.PlayerGetHit += PlayerGetHit;
        monsterView.EndAttackAction += EndAttack;

        navAgent = GetComponent<NavMeshAgent>();
        stateMachine = new StateMachine();
        stateMachine.Init(this);
        stateMachine.ChangeState<MonsterIdleState>();

        EventManager.Instance.Register("GameOver", Celebrate);
    }

    private void Celebrate()
    {
        stateMachine.ChangeState<MonsterCelebrateState>();
    }

    private void PlayerGetHit()
    {
        PlayerController.Instance.GetHit(20);

    }

    private void OnDestroy() 
    {
        stateMachine?.Stop();
        EventManager.Instance?.Unregister("GameOver", Celebrate);
        monsterView?.Destroy();
    }
    /// <summary>
    /// 受伤逻辑
    /// </summary>
    public void GetHit(int dmg)
    {
        if (curHP == 0) return; //因为是延迟死亡，所以需要排除掉尸体被攻击的情况
        curHP -= dmg;
        if (curHP <= 0)
        {
            curHP = 0;
            
            stateMachine.ChangeState<MonsterDieState>();
        }
        else
        {
            stateMachine.ChangeState<MonsterGetHitState>(true);
        }
    }
    /// <summary>
    /// 死亡逻辑
    /// </summary>
    public void Die()
    {
        stateMachine.Stop();
        monsterView.Destroy();
        stateMachine = null;
        monsterView = null;
        PoolManager.Instance.PushGameObj(gameObject);
        
        EventManager.Instance?.Unregister("GameOver", Celebrate);
    }
    #region 实际处理动画事件
    public void EndGetHit()
    {
        stateMachine.ChangeState<MonsterFollowState>(); //切换为跟随状态
    }
    private void EndAttack()
    {
        stateMachine.ChangeState<MonsterFollowState>();//切换为跟随状态
    }
    #endregion
    public Vector3 GetPatrolPoint()
    {
        return MonsterManager.Instance.GetPatrolPoint();
    }
}
