using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 一类怪物的配置
/// </summary>
[CreateAssetMenu(fileName = "MonsterConfig", menuName = "GameConfig/MonsterConfig")]
public class MonsterConfig : ConfigBase
{
    public int hp;
    public int atk;
   
    //public GameObject prefab;
    public string prefabStr;

    #region AI相关配置
    public float stopPatrolRange; //巡逻时到目标点的距离多久停下
    public float waitNextPatrolTime; //从空闲等待下次巡逻的时间
    public float sightRange;     //发现玩家的距离（怪物视野）
    public float atkRange;  //攻击距离（追击时多近就开始攻击玩家）
    
    #endregion

}
