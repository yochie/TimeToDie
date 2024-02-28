using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField]
    private Collider2D hitbox;

    [SerializeField]
    private LayerMask hitableLayers;

    [SerializeField]
    private float knockbackDurationSeconds;

    [SerializeField]
    private Vector2 knockbackVelocity;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (hitableLayers.Contains(other.gameObject.layer))
        {
            Mover mover = other.GetComponent<Mover>();
            if(mover != null)
            {
                Vector2 kbDir = this.transform.position.x - other.transform.position.x > 0 ? Vector2.left : Vector2.right;
                mover.Knockback(this.knockbackDurationSeconds, this.knockbackVelocity * kbDir);
            }

            Arrow arrow = other.GetComponent<Arrow>();
            if(arrow != null)
            {
                arrow.Deflect();
            }
        }
    }
}
