using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PainByDamageTypeSO : ScriptableObject
{
    [SerializeField]
    private float fallDamagePainRatio;

    public float GetPainForDamageType(DamageType type)
    {
        switch (type)
        {
            case DamageType.fall:
                return this.fallDamagePainRatio;
        }
        return 1;
    }
}
