using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField]
    float threshold;

    [SerializeField]
    float damagePerUnitAboveThreshold;

    [SerializeField]
    DamageHandler damageHandler;

    public void OnLand(float from, float to)
    {
        float thresholdExcess = (from - to) - this.threshold;
        if (thresholdExcess > 0) {
            int damage = (int)(thresholdExcess * this.damagePerUnitAboveThreshold);
            //only send damage to handler if rounded damage is noticeable
            if(damage > 0)
                this.damageHandler.TakeDamage(damage, DamageType.fall);
        }
    }
}
