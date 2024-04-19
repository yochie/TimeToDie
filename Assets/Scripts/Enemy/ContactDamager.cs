using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactDamager : MonoBehaviour
{

    [SerializeField]
    private LayerMask damagesLayers;

    [SerializeField]
    private EnemyStats enemyStats;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Collider2D contactCollider;

    [SerializeField]
    private float disarmForSecondsOnHurt;

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("triggered");
        if (this.damagesLayers.Contains(other.gameObject.layer))
        {
            //if colliding with hurbox instead of main object, grab main object from hurtbox property
            HurtBox hb = other.GetComponent<HurtBox>();
            GameObject otherObject;
            if (hb != null)
                otherObject = hb.HurtboxFor;
            else
                otherObject = other.gameObject;

            Hitable hitable = otherObject.GetComponent<Hitable>();
            if (hitable != null) {
                if (!hitable.CanBeHit())
                    return;
                else
                    hitable.TakeHit();
            }
            
            DamageHandler damageHandler = otherObject.GetComponent<DamageHandler>();
            if(damageHandler != null)
            {
                damageHandler.TakeDamage(this.enemyStats.ContactDamage, DamageType.contact);
            }

            Mover mover = otherObject.GetComponent<Mover>();
            if(mover != null)
            {
                float dir = this.transform.position.x - otherObject.transform.position.x > 0 ? -1 : 1;
                Vector2 knockBackForceWithDir = this.enemyStats.KnockbackForce;
                knockBackForceWithDir.x *= dir;
                mover.Knockback(this.enemyStats.KnockbackDuration, knockBackForceWithDir);
            }
        }
    }

    public void TemporaryDisarmForHurt()
    {
        this.StartCoroutine(TemporaryDisarmCoroutine(this.disarmForSecondsOnHurt));
    }

    private IEnumerator TemporaryDisarmCoroutine(float seconds)
    {
        this.contactCollider.enabled = false;
        yield return new WaitForSeconds(seconds);
        this.contactCollider.enabled = true;
    }
}
