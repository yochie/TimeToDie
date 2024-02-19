using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PainByDamageTypeSO : ScriptableObject
{
    [SerializeField]
    private float fallDamagePainRatio;

    [SerializeField]
    private float spikesDamagePainRatio;

    public float GetPainForDamageType(DamageType type)
    {
        switch (type)
        {
            case DamageType.fall:
                return this.fallDamagePainRatio;
            case DamageType.spikes:
                return this.spikesDamagePainRatio;
        }
        return 1;
    }
}
