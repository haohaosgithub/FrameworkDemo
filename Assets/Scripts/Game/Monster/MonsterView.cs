using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterView : MonoBehaviour
{
    public Animator anim;
    public void Init()
    {
        anim = GetComponent<Animator>();
    }
}
