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
            //Debug.Log("ouch");
            this.damageHandler.TakeDamage((int) (thresholdExcess * this.damagePerUnitAboveThreshold), DamageType.fall);
        }
    }
}
