using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitable : MonoBehaviour
{
    [SerializeField]
    private float immuneForSeondsOnHit;

    private float remainingImmunitySeconds;

    private void Start()
    {
        this.remainingImmunitySeconds = 0;
    }

    private void FixedUpdate()
    {
        this.remainingImmunitySeconds -= Time.fixedDeltaTime;
    }

    internal bool CanBeHit()
    {
        return this.remainingImmunitySeconds <= 0;
    }

    internal void TakeHit()
    {
        this.remainingImmunitySeconds = this.immuneForSeondsOnHit;
    }
}
