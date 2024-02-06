using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerStats playerStats;

    [SerializeField]
    private PainByDamageTypeSO painByDamageType;

    internal void TakeDamage(int damageAmount, DamageType damageType)
    {
        int pain = (int) (damageAmount * painByDamageType.GetPainForDamageType(damageType));

        Debug.LogFormat("Taking {0} {1} damage", damageAmount, damageType);
        //update model
        this.playerStats.TakeDamage(damageAmount);
        this.playerStats.TakePain(pain);
    }
}
