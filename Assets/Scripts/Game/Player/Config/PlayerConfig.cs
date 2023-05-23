using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig" ,menuName = "GameConfig/PlayerConfig")]
public class PlayerConfig : ConfigBase
{
    public int moveSpeed;
    public int maxBulletNum;
    public float shootInternal;
    public int bulletMoveForce;
    public int bulletAtk;
    public int maxHP;
    

    
}
