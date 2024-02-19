using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeSlower : MonoBehaviour
{
    [SerializeField]
    private LayerMask affectedMoversMask;

    [SerializeField]
    private float speedMultiplier;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (this.affectedMoversMask.Contains(other.gameObject.layer)){
            Mover mover = other.GetComponent<Mover>();
            if (mover != null)
                mover.MultiplySpeed(speedMultiplier);
            Debug.Log("Mover stepped on spikes");
        }
    } 

    private void OnTriggerExit2D(Collider2D other)
    {
        if (this.affectedMoversMask.Contains(other.gameObject.layer))
        {
            Mover mover = other.GetComponent<Mover>();
            if (mover != null)
                mover.MultiplySpeed(1f/speedMultiplier);
            Debug.Log("Mover left spikes");
        }
    }
}
