using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PainByDamageTypeSO : ScriptableObject
{
    [SerializeField]
    private float fallPainRatio;

    [SerializeField]
    private float spikePainRatio;

    [SerializeField]
    private float arrowPainRatio;

    [SerializeField]
    private float contactPainRatio;

    public float GetPainForDamageType(DamageType type)
    {
        switch (type)
        {
            case DamageType.fall:
                return this.fallPainRatio;
            case DamageType.spikes:
                return this.spikePainRatio;
            case DamageType.arrow:
                return this.arrowPainRatio;
            case DamageType.contact:
                return this.contactPainRatio;
        }
        return 1;
    }
}
