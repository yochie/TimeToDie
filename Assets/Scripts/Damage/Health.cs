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

    // Start is called before the first frame update
    void Start()
    {
        this.currentHealth = this.MaxHealth;
    }
}
