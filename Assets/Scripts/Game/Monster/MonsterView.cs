using Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Pool]
public class MonsterView : MonoBehaviour
{
    public Animator anim;
    public WeaponCollider weaponCollider; //武器碰撞体
    #region 委托给controller层处理（动画事件，碰撞事件）
    public event Action EndGetHitAction; //Monster结束被攻击
    public Action PlayerGetHit; //通知玩家被攻击
    public Action EndAttackAction; //通知攻击结束
    #endregion
    bool canHit; //保证每次动画在其两动画帧之间检测一次trigger enter
    public void Init()
    {
        anim = GetComponent<Animator>();
        weaponCollider.weaponOnTriggerCb = WeaponOntriggerCb;
    }
    private void WeaponOntriggerCb(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(canHit)
            {
                canHit = false;
                AudioManager.Instance.PlayOneShot("Audio/Monster/拳头击中", transform);
                PlayerGetHit();
            }
            

        }
    }
    public void Destroy()
    {
        EndGetHitAction = null;
        anim = null;
        weaponCollider.weaponOnTriggerCb = null;
        PoolManager.Instance.PushGameObj(gameObject);

    }
    #region 动画事件
    public void EndGetHit()
    {
        EndGetHitAction();
    }

    public void StartHit()
    {        
        canHit = true;
    }

    public void StopHit()
    {
        canHit = false;
    }

    public void EndAttack()
    {
        EndAttackAction();
    }

    
    #endregion
}
