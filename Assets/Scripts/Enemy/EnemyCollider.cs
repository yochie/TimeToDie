using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{

    [SerializeField]
    private LayerMask damagesLayers;

    [SerializeField]
    private EnemyStats enemyStats;

    [SerializeField]
    private Rigidbody2D rb;

    private Vector2 previousVelocity;

    private void Awake()
    {
        this.previousVelocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        this.previousVelocity = this.rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (this.damagesLayers.Contains(other.gameObject.layer))
        {
            DamageHandler damageHandler = other.gameObject.GetComponent<DamageHandler>();
            if(damageHandler != null)
            {
                damageHandler.TakeDamage(this.enemyStats.ContactDamage, DamageType.contact);
            }

            Mover mover = other.gameObject.GetComponent<Mover>();
            if(mover != null)
            {
                float dir = this.transform.position.x - other.transform.position.x > 0 ? -1 : 1;
                Vector2 knockBackForceWithDir = this.enemyStats.KnockbackForce;
                knockBackForceWithDir.x *= dir;
                mover.Knockback(this.enemyStats.KnockbackDuration, knockBackForceWithDir);
            }
        }
    }
}
