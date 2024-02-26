using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrapTrigger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer trapSprite;

    [SerializeField]
    private Sprite emptyTrapSprite;

    [SerializeField]
    private Collider2D triggerCollider;

    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private float arrowSpeed;

    [SerializeField]
    private float arrowRotationSpeed;

    [SerializeField]
    [Range(-1, 1)]
    private int direction;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trap triggered");
        this.trapSprite.sprite = this.emptyTrapSprite;
        this.triggerCollider.enabled = false;
        this.arrow.SetActive(true);
        Rigidbody2D arrowBody =  this.arrow.GetComponent<Rigidbody2D>();
        arrowBody.velocity = new Vector2(this.arrowSpeed * this.direction, 0);
        arrowBody.angularVelocity = -this.direction * this.arrowRotationSpeed;
    }
}
