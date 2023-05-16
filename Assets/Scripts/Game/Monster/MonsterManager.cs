using Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 怪物生成器
/// </summary>
public class MonsterManager : LogicManager<MonsterManager>
{
    public LVConfig lvConfig; //当前关卡的配置

    public Transform monsterGenPoint; //怪物生成点
    public int curMonsterNum; //当前场景怪物数量


    public HashSet<MonsterController> allMonster = new HashSet<MonsterController>();

    public List<Transform> patrolPoint; //巡逻点
    protected override void Awake()
    {
        base.Awake();
        monsterGenPoint = transform.Find("monsterGenPoint");
        
        InvokeRepeating(nameof(GenerateMonster),1,lvConfig.generateMonsterInternal);
    }
    public void GenerateMonster()
    {
        if(curMonsterNum < lvConfig.maxMonsterNum)
        {
            curMonsterNum++;
            //得到怪物配置
            MonsterConfig monsterConfig = GetOneConfig();
            //根据配置实例化怪物
            MonsterController monsterController = ResManager.Instance.Load<MonsterController>("Game/Monster/Monster",transform);
            monsterController.transform.position = monsterGenPoint.position;
            monsterController.Init(monsterConfig);
            allMonster.Add(monsterController);
        }
    }
    //根据概率随机得到要生成的怪物配置
    public MonsterConfig GetOneConfig() 
    {
        float sum = 0;
        //归一化概率
        foreach (var v in lvConfig.monsterConfigDic.Values)
        {
            sum += v;
        }
        
        for(int i = 0;i< lvConfig.monsterConfigDic.Count;i++)
        {
            KeyValuePair<MonsterConfig, float> kvp = lvConfig.monsterConfigDic.ElementAt(i);
            float v = kvp.Value / sum;
            lvConfig.monsterConfigDic[kvp.Key] = v;
        }
        //加权随机选数
        float rand = Random.Range(0f, 1f);
        float sumProb = 0f;
        MonsterConfig lastConfig = null;
        foreach (var kv in lvConfig.monsterConfigDic)
        {
            sumProb += kv.Value;
            if(rand < sumProb)
            {
                return kv.Key;
            }
            lastConfig = kv.Key;
        }
        return lastConfig;
    }

    public Vector3 GetPatrolPoint()
    {
        return patrolPoint[Random.Range(0, patrolPoint.Count)].position;
    }

    protected override void RegisterListener()
    {
        
    }

    protected override void UnRegisterListener()
    {
        
    }
}
