using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Jumper : MonoBehaviour
{

    [field: SerializeField]
    public int DefaultJumpSpeed { get; private set; }

    [field: SerializeField]
    public float MaxJumpHoldTime { get; private set; }

    //allows jumping for some time after becoming ungrounded
    [field: SerializeField]
    public float CoyoteTime { get; private set; }

    [SerializeField]
    private Collider2D groundOverlapCollider;

    [SerializeField]
    private Collider2D ceilingOverlapCollider;

    [SerializeField]
    private LayerMask groundLayerMask;

    [SerializeField]
    private UnityEvent<float, float> onFallEvent;

    public int CurrentJumpSpeed { get; set; }

    private bool grounded;

    //for holding jump
    private bool jumping;

    //for fall damage
    private float fallingFrom;

    //for coyote time
    private float lastGroundedTime;

    //for jump cooldown + timeout
    private float jumpStartedAtTime;

    //set from input
    private bool jumpedFromGround;

    private void Start()
    {
        this.CurrentJumpSpeed = this.DefaultJumpSpeed;
        this.jumpedFromGround = false;
        this.jumpStartedAtTime = 0f;

        this.grounded = false;
        this.jumping = false;
        this.fallingFrom = float.MinValue;
        this.lastGroundedTime = float.MinValue;
    }

    internal bool IsGrounded()
    {
        return this.grounded;
    }
    internal bool IsJumping()
    {
        return this.jumping;
    }

    //returns velocity that should be used for next frame
    //Should be called by a fixed update that manages attached rigidbody
    //Side effects : throws fall event and updates jumping state tracking (falling, grounded, coyote time, etc)
    public float UpdateForFrame(float previousVelocity, bool jumpHeld)
    {
        float yVelocity = previousVelocity;

        //throws fall event
        bool wasGrounded = this.grounded;
        this.grounded = this.groundOverlapCollider.IsTouchingLayers(this.groundLayerMask);
        if (!wasGrounded && grounded)
        {
            this.onFallEvent.Invoke(this.fallingFrom, this.transform.position.y);
            this.fallingFrom = float.MinValue;
        }

        //to interrupt jumping
        bool isCeilinged = this.ceilingOverlapCollider.IsTouchingLayers(this.groundLayerMask);
        this.jumping = this.UpdateJumpState(wasJumping: this.jumping, jumpInputed: this.jumpedFromGround, isCeilinged: isCeilinged, jumpHeld: jumpHeld, maxJumpHoldTime: this.MaxJumpHoldTime);
        if (this.jumping)
        {
            yVelocity = this.CurrentJumpSpeed;
        }

        if (!grounded)
            //for fall damage
            this.fallingFrom = Mathf.Max(this.transform.position.y, this.fallingFrom);
        else
            //for coyote time
            this.lastGroundedTime = Time.time;

        return yVelocity;
    }

    //returns true if jumping for this frame
    //Side effects: Will update jump input state and jump start time
    private bool UpdateJumpState(bool wasJumping, bool jumpInputed, bool isCeilinged, bool jumpHeld, float maxJumpHoldTime)
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
        else if (wasJumping && !jumpHeld)
        {
            //button released : let gravity take over
            jumping = false;
        }
        else if (wasJumping && isCeilinged)
        {
            //hit ceiling : interrupt jump
            jumping = false;
        }
        else if (wasJumping && jumpHeld && Time.time - this.jumpStartedAtTime < maxJumpHoldTime)
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

    public void Jump()
    {
        if (this.grounded)
            this.jumpedFromGround = true;
        else if (Time.time - this.lastGroundedTime <= this.CoyoteTime && Time.time - jumpStartedAtTime > this.CoyoteTime)
            this.jumpedFromGround = true;
    }
}
