using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour
{
    public Animator anim;
    public event Action endGetHitAction; //委托给controller层处理
    public void Init()
    {
        anim = GetComponent<Animator>();
    }

    #region 动画事件
    public void EndGetHit()
    {
        endGetHitAction();
    }
    #endregion
}
