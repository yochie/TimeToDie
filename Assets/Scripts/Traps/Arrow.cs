using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField]
    private LayerMask dealsDamageToLayers;

    [SerializeField]
    private float damage;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float moveThreshold;

    [SerializeField]
    private float disableAfterSecondsImmobile;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float knockbackDurationSeconds;

    [SerializeField]
    private Vector2 knockbackVelocity;

    private bool isArmed;
    private float immobileOnGroundFor;
    private Vector2 previousVelocity;

    private void Start()
    {
        this.isArmed = true;
        this.immobileOnGroundFor = 0;
        this.previousVelocity = this.rb.velocity;
    }

    private void FixedUpdate()
    {
        this.previousVelocity = this.rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject otherObject = collision.gameObject;
        if (this.isArmed && dealsDamageToLayers.Contains(otherObject.layer))
        {
            DamageHandler damageHandler = otherObject.GetComponent<DamageHandler>();
            if (damageHandler != null)
                damageHandler.TakeDamage(this.damage, DamageType.arrow);

            Mover mover = otherObject.GetComponent<Mover>();
            if (mover != null)
            {
                float dir = this.previousVelocity.x > 0 ? 1 : -1;
                Vector2 knockbackWithDir = this.knockbackVelocity;
                knockbackWithDir.x *= dir;
                mover.Knockback(this.knockbackDurationSeconds, knockbackWithDir);
            }

            this.isArmed = false;
        }
    }

    //disarms and disables simulation when conditons are met (delay immobile on ground)
    private void Update()
    {
        if (!this.isArmed && !this.rb.simulated)
            return;

        //checks for immobile since some threshold time, then disarm
        if (this.rb.velocity.magnitude < this.moveThreshold && this.rb.IsTouchingLayers(this.groundMask)) {
            this.immobileOnGroundFor += Time.deltaTime;
            if (this.immobileOnGroundFor > this.disableAfterSecondsImmobile)
            {
                this.isArmed = false;
                this.rb.simulated = false;
            }
        }
        else
        {
            this.immobileOnGroundFor = 0;
        }
    }

    internal void Deflect()
    {
        this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
        this.rb.velocity = new Vector2(this.rb.velocity.x * -1, this.rb.velocity.y);
    }
}
