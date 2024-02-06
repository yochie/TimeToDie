using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float maxJumpHoldTime;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    [SerializeField]
    private Collider2D groundOverlapCollider;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    PlayerInput playerInput;

    [SerializeField]
    private UnityEvent<float, float> onFallEvent;

    private Vector2 moveInput;

    private bool isGrounded;
    private InputAction jumpAction;
    private bool jumpedFromGround;
    private bool jumping;
    private float jumpStartedAtTime;
    private float fallingFrom;

    private void Start()
    {
        this.moveInput = Vector2.zero;

        this.isGrounded = false;
        jumpAction = this.playerInput.actions["Jump"];
        this.jumpedFromGround = false;
        this.jumping = false;
        this.jumpStartedAtTime = 0f;
        this.fallingFrom = float.MinValue;
    }

    private void FixedUpdate()
    {
        //allow player input to control horizontal velocity
        float xVelocity = this.moveInput.x * this.moveSpeed * Time.fixedDeltaTime;

        //let physics handle vertical velocity unless jumping
        float yVelocity = this.playerRigidbody2D.velocity.y;

        //for land event
        bool wasGrounded = this.isGrounded;
        this.isGrounded = this.groundOverlapCollider.IsTouchingLayers(this.groundLayerMask);
        if(!wasGrounded && isGrounded)
        {
            this.onFallEvent.Invoke(this.fallingFrom, this.transform.position.y);
            this.fallingFrom = float.MinValue;
        }
        if (this.jumpedFromGround)
        {
            //start jump
            Debug.Log("jump");
            this.jumpStartedAtTime = Time.time;
            this.jumpedFromGround = false;
            this.jumping = true;
            yVelocity = this.jumpSpeed * Time.fixedDeltaTime;
        }
        else if (this.jumping && !jumpAction.IsPressed())
        {
            //button released : let gravity take over
            this.jumping = false;
        }
        else if (this.jumping && jumpAction.IsPressed() && Time.time - this.jumpStartedAtTime < this.maxJumpHoldTime)
        {
            //button held : continue jump
            yVelocity = this.jumpSpeed * Time.fixedDeltaTime;
        }
        else
        {
            //not jumping or jump ended due to timeout
            this.jumping = false;
        }

        this.playerRigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        if (!isGrounded)
            this.fallingFrom = Mathf.Max(this.transform.position.y, this.fallingFrom);
    }

    //Called by input system
    public void OnMove(InputValue value) => this.moveInput = value.Get<Vector2>();

    //Called by input system
    public void OnJump(InputValue value)
    {
        if(this.isGrounded)
            this.jumpedFromGround = true;
    }

}
