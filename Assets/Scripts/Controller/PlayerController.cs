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
    PlayerInput playerInput;

    [SerializeField]
    private UnityEvent<float, float> onFallEvent;

    private Vector2 moveInput;

    private bool isGrounded;
    private bool isCeilinged;

    private InputAction jumpAction;
    private bool jumpedFromGround;
    private bool jumping;
    private float jumpStartedAtTime;
    private float fallingFrom;
    private float lastGroundedTime;

    private void Start()
    {
        this.moveInput = Vector2.zero;

        this.isGrounded = false;
        this.isCeilinged = false;
        jumpAction = this.playerInput.actions["Jump"];
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

        this.isCeilinged = this.ceilingOverlapCollider.IsTouchingLayers(this.groundLayerMask);
        if (this.isCeilinged)
            Debug.Log("hit ceiling");

        if (this.jumpedFromGround)
        {
            //start jump
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
        else if (this.jumping && this.isCeilinged)
        {
            //hit ceiling : interrupt jump
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
        else
            this.lastGroundedTime = Time.time;
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
