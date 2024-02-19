using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerState playerStats;

    [SerializeField]
    private PainByDamageTypeSO painByDamageType;

    internal void TakeDamage(float damage, DamageType damageType)
    {
        float pain = damage * painByDamageType.GetPainForDamageType(damageType);

        Debug.LogFormat("Taking {0} {1} damage for {2} pain", damage, damageType, pain);
        //update model
        this.playerStats.Hurt(damage, pain);
    }
}
