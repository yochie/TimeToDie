using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeEffect : MonoBehaviour
{
    [SerializeField]
    private LayerMask affectedObjectsMask;

    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    private float spikeDamagePerSecond;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.affectedObjectsMask.Contains(other.gameObject.layer)){
            Mover mover = other.GetComponent<Mover>();
            if (mover != null)
                mover.MultiplySpeed(speedMultiplier);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (this.affectedObjectsMask.Contains(other.gameObject.layer))
        {
            DamageHandler objectDamageHandler = other.GetComponent<DamageHandler>();
            if (objectDamageHandler != null)
                objectDamageHandler.TakeDamage(this.spikeDamagePerSecond * Time.deltaTime, DamageType.spikes);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (this.affectedObjectsMask.Contains(other.gameObject.layer))
        {
            Mover mover = other.GetComponent<Mover>();
            if (mover != null)
                mover.MultiplySpeed(1f/speedMultiplier);
        }
    }
}
