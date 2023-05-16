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
    
    public PlayerController playerController;
    public void Init(MonsterConfig monsterConfig)
    {
        playerController = PlayerController.Instance;
        monsterView = PoolManager.Instance.GetGameObject<MonsterView>(monsterConfig.prefab,transform);
        monsterView.transform.localPosition = Vector3.zero;
        monsterView.Init();
        
        
        navAgent = GetComponent<NavMeshAgent>();

        stateMachine = new StateMachine();
        stateMachine.Init(this);
        stateMachine.ChangeState<MonsterIdleState>();
    }

    public Vector3 GetPatrolPoint()
    {
        return MonsterManager.Instance.GetPatrolPoint();
    }
}
