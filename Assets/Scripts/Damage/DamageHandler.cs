using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField]
    private PlayerState playerState;

    [SerializeField]
    private PainByDamageTypeSO painByDamageType;

    internal void TakeDamage(float damage, DamageType damageType)
    {
        if (!this.playerState.CanTakeDamage)
            return;
        float pain = damage * painByDamageType.GetPainForDamageType(damageType);

        Debug.LogFormat("Taking {0} {1} damage for {2} pain", damage, damageType, pain);
        //update model
        this.playerState.Hurt(damage, pain);
    }
}
