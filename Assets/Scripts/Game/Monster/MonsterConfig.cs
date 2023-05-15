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
    public GameObject prefab;
}
