using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public void Init(MonsterConfig monsterConfig)
    {
        MonsterView monsterView = PoolManager.Instance.GetGameObject<MonsterView>(monsterConfig.prefab,transform);
        monsterView.Init();
    }
}
