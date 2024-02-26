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
    private Rigidbody2D arrowBody;

    [SerializeField]
    private float moveThreshold;

    [SerializeField]
    private float disableAfterSecondsImmobile;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private float knockbackDurationSeconds;

    [SerializeField]
    private float knockbackSpeed;

    private bool isArmed;
    private float immobileOnGroundFor;

    private void Start()
    {
        this.isArmed = true;
        this.immobileOnGroundFor = 0;
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
                float dir = collision.relativeVelocity.x > 0 ? -1 : 1;
                mover.Knockback(this.knockbackDurationSeconds, dir, this.knockbackSpeed);
            }

            this.isArmed = false;
        }
    }


    //disarms and disables simulation when conditons are met (delay immobile on ground)
    private void Update()
    {
        if (!this.isArmed && !this.arrowBody.simulated)
            return;

        //checks for immobile since some threshold time, then disarm
        if (this.arrowBody.velocity.magnitude < this.moveThreshold && this.arrowBody.IsTouchingLayers(this.groundMask)) {
            this.immobileOnGroundFor += Time.deltaTime;
            if (this.immobileOnGroundFor > this.disableAfterSecondsImmobile)
            {
                this.isArmed = false;
                this.arrowBody.simulated = false;
            }
        }
        else
        {
            this.immobileOnGroundFor = 0;
        }
    }
}
