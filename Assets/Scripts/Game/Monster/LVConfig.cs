using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 当前关卡的配置
/// </summary>
[CreateAssetMenu(fileName = "LVConfig", menuName = "GameConfig/LVConfig")]
public class LVConfig : ConfigBase
{
    public int maxMonsterNum; //场景中最多有的怪物数量
    public float generateMonsterInternal; //场景不满最大怪物数量后，多久生成怪物
    public Dictionary<MonsterConfig, float>  monsterConfigDic; //key:某类怪物 value: 这类怪物的生成概率 

    
    

}
