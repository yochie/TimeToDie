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
                float kbDir = this.transform.position.x - other.transform.position.x > 0 ? -1 : 1;
                mover.Knockback(this.knockbackDurationSeconds, new Vector2(this.knockbackVelocity.x * kbDir, this.knockbackVelocity.y));
            }

            Arrow arrow = other.GetComponent<Arrow>();
            if(arrow != null)
            {
                arrow.Deflect();
            }
        }
    }

    public void Flip(float faceDir)
    {
        Vector3 flippedScale = this.transform.localScale;
        flippedScale.x = faceDir;
        this.transform.localScale = flippedScale;
    }
}
