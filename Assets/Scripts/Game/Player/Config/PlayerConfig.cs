using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig" ,menuName = "GameConfig/PlayerConfig")]
public class PlayerConfig : ConfigBase
{
    public int maxHP;
    public int attack;
}
