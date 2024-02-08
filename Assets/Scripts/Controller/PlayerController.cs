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

    //allows jumping for some time after becoming ungrounded
    [SerializeField]
    private float coyoteTime;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    [SerializeField]
    private Collider2D groundOverlapCollider;

    [SerializeField]
    private Collider2D ceilingOverlapCollider;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private UnityEvent<float, float> onFallEvent;

    private Vector2 moveInput;

    private bool isGrounded;

    private InputAction jumpAction;

    //set from input
    private bool jumpedFromGround;

    //for holding jump
    private bool jumping;

    //for jump cooldown + timeout
    private float jumpStartedAtTime;
    
    //for fall damage
    private float fallingFrom;

    //for coyote time
    private float lastGroundedTime;

    //for flipping sprite whenever changing dir
    private float previousDirection;

    private void Start()
    {
        this.moveInput = Vector2.zero;

        this.isGrounded = false;
        this.jumpAction = this.playerInput.actions["Jump"];
        this.jumpedFromGround = false;
        this.jumping = false;
        this.jumpStartedAtTime = 0f;
        this.fallingFrom = float.MinValue;
        this.lastGroundedTime = float.MinValue;
    }

    private void FixedUpdate()
    {
        //allow player input to control horizontal velocity
        float xVelocity = this.moveInput.x * this.moveSpeed * Time.fixedDeltaTime;

        //let physics handle vertical velocity unless jumping
        float yVelocity = this.playerRigidbody2D.velocity.y;

        //throws fall event
        bool wasGrounded = this.isGrounded;
        this.isGrounded = this.groundOverlapCollider.IsTouchingLayers(this.groundLayerMask);
        if(!wasGrounded && isGrounded)
        {
            this.onFallEvent.Invoke(this.fallingFrom, this.transform.position.y);
            this.fallingFrom = float.MinValue;
        }

        //to interrupt jumping
        bool isCeilinged = this.ceilingOverlapCollider.IsTouchingLayers(this.groundLayerMask);
        this.jumping = this.UpdateJumpState(wasJumping: this.jumping, jumpInputed : this.jumpedFromGround, isCeilinged : isCeilinged, jumpAction: this.jumpAction, maxJumpHoldTime: this.maxJumpHoldTime);
        if (this.jumping)
        {
            yVelocity = this.jumpSpeed * Time.fixedDeltaTime;
        }

        //set velocity for this frame based on input and jump state
        this.playerRigidbody2D.velocity = new Vector2(xVelocity, yVelocity);
        
        if (!isGrounded)
            //for fall damage
            this.fallingFrom = Mathf.Max(this.transform.position.y, this.fallingFrom);
        else
            //for coyote time
            this.lastGroundedTime = Time.time;

        this.animator.SetBool("jumping", this.jumping);
        this.animator.SetBool("running", this.moveInput.x != 0);
        this.animator.SetBool("grounded", this.isGrounded);
        if (this.moveInput.x != 0 && this.moveInput.x != this.previousDirection)
            this.spriteRenderer.flipX = this.moveInput.x != 1f;
        this.previousDirection = this.moveInput.x;
    }


    //Side effects: Will update jump input state and jump start time
    //returns true if jumping for this frame
    private bool UpdateJumpState(bool wasJumping, bool jumpInputed, bool isCeilinged, InputAction jumpAction, float maxJumpHoldTime)
    {
        bool jumping;
        if (jumpInputed)
        {
            //start jump
            this.jumpStartedAtTime = Time.time;
            //reset input
            this.jumpedFromGround = false;
            jumping = true;
        }
        else if (wasJumping && !jumpAction.IsPressed())
        {
            //button released : let gravity take over
            jumping = false;
        }
        else if (wasJumping && isCeilinged)
        {
            //hit ceiling : interrupt jump
            jumping = false;
        }
        else if (wasJumping && jumpAction.IsPressed() && Time.time - this.jumpStartedAtTime < maxJumpHoldTime)
        {
            //button held : continue jump
            jumping = true;
        }
        else
        {
            //not jumping or jump ended due to timeout
            jumping = false;
        }
        return jumping;
    }

    //Called by input system
    public void OnMove(InputValue value) => this.moveInput = value.Get<Vector2>();

    //Called by input system
    public void OnJump(InputValue value)
    {
        if(this.isGrounded)
            this.jumpedFromGround = true;
        else if(Time.time - this.lastGroundedTime <= this.coyoteTime && Time.time - jumpStartedAtTime > this.coyoteTime)
            this.jumpedFromGround = true;
    }
}
