using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [field: SerializeField]
    public float MaxHealth { get; private set; }

    private float currentHealth;
    public float CurrentHealth { 
        get
        {
            return this.currentHealth;
        } 
        set {
            this.currentHealth = Mathf.Clamp(value, 0, this.MaxHealth);
        }
    }

    internal void Init()
    {
        this.currentHealth = this.MaxHealth;
    }
}
