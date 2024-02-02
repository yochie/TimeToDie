using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveForce;
    
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private CapsuleCollider2D playerCollider2D;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    [SerializeField]
    private CapsuleCollider2D groundOverlapCapsule;

    [SerializeField]
    private LayerMask groundLayerMask;

    private bool isGrounded = false;

    private Vector2 movement;
    private bool jumped;


    private void FixedUpdate()
    {        
        this.isGrounded = this.groundOverlapCapsule.IsTouchingLayers(this.groundLayerMask);

        this.playerRigidbody2D.AddForce(this.movement * this.moveForce * Time.fixedDeltaTime);

        if (this.jumped)
        {
            Debug.Log("jumping");
            this.playerRigidbody2D.AddForce(Vector2.up * this.jumpForce);
            this.jumped = false;
        }
    }

    //Called by input system
    public void OnMove(InputValue value) => this.movement = value.Get<Vector2>();

    //Called by input system
    public void OnJump(InputValue value)
    {
        if (this.isGrounded)
            this.jumped = true;
    }

}
