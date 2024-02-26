using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{        
    [field: SerializeField]
    public float ContactDamage { get; private set; }

    [field:SerializeField]
    public float KnockbackDuration { get; private set; }
    
    [field: SerializeField]
    public Vector2 KnockbackForce { get; private set; }
}
