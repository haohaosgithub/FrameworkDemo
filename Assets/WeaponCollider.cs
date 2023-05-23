using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public  Action<Collider> weaponOnTriggerCb;
    private void OnTriggerEnter(Collider other)
    {
        weaponOnTriggerCb(other);
    }
}
