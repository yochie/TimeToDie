using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [field: SerializeField]
    public float DefaultMoveSpeed { get; private set; }

    [SerializeField]
    private AnimatedSprite animatedSprite;

    [SerializeField]
    private SpriteRenderer fixedSprite;

    [SerializeField]
    private Jumper jumper;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private PhysicsMaterial2D kbMaterial;

    [SerializeField]
    private PhysicsMaterial2D normalMaterial;

    [SerializeField]
    private float knockbackImmunityDuration;

    [SerializeField]
    private bool recoversFromKnockWhileAirborne;

    private float currentMoveSpeed;

    //for flipping sprite whenever changing dir
    private float previousDirection;

    private float movedInDirection;

    private float knockbackRemainingSeconds;

    private float knockbackImmunityRemaining;

    private bool recoveredFromKnockback;

    private void Start()
    {
        this.currentMoveSpeed = this.DefaultMoveSpeed;
        this.movedInDirection = 0;
        this.previousDirection = 0;
        this.recoveredFromKnockback = true;
        this.knockbackImmunityRemaining = 0;
    }

    //updates tracked state and returns velocity for frame
    public void UpdateForFixedFrame(bool jumpHeld)
    {
        //flip sprite based on input
        if (this.movedInDirection != 0 && this.movedInDirection != this.previousDirection)
        {
            this.Flip(faceDir: this.movedInDirection);
        }
        this.previousDirection = this.movedInDirection;

        float xVelocity = this.rb.velocity.x;
        float yVelocity = this.rb.velocity.y;
        if (knockbackRemainingSeconds > 0)
        {
            //let physics handle velocity during knockback
            this.knockbackRemainingSeconds -= Time.fixedDeltaTime;

        } else if (!recoveredFromKnockback)
        {
            yVelocity = this.jumper.UpdateForFrame(yVelocity, jumpHeld);
            this.recoveredFromKnockback = this.jumper.IsGrounded();
        }
        else
        {
            if (this.rb.sharedMaterial != this.normalMaterial)
                this.rb.sharedMaterial = this.normalMaterial;
            //manually update velocity based on input            
            xVelocity = this.currentMoveSpeed * this.movedInDirection;            
            if (this.jumper != null)
                yVelocity = this.jumper.UpdateForFrame(previousVelocity: yVelocity, jumpHeld);
        }
        this.rb.velocity = new Vector2(xVelocity, yVelocity);
        
        if(this.knockbackImmunityRemaining > 0)
            this.knockbackImmunityRemaining -= Time.fixedDeltaTime;

    }

    public void Flip(float faceDir)
    {
        //assumes fixed sprite face right
        if (this.animatedSprite != null)
            this.animatedSprite.Flip(faceDir);
        else if (this.fixedSprite != null)
            this.fixedSprite.flipX = faceDir > 0 ? false : true;
        this.previousDirection = faceDir;
    }

    public void MoveInDirection(float direction)
    {
        this.movedInDirection = direction;
    }

    public void Jump()
    {
        this.jumper.Jump();
    }

    public void Knockback(float durationSeconds, Vector2 knockbackVelocity)
    {
        if (knockbackImmunityRemaining > 0)
            return;
        this.knockbackRemainingSeconds = durationSeconds;
        this.rb.velocity = knockbackVelocity;
        this.rb.sharedMaterial = this.kbMaterial;
        if(!this.recoversFromKnockWhileAirborne)
            this.recoveredFromKnockback = false;
        this.knockbackImmunityRemaining = this.knockbackImmunityDuration;
    }

    public bool IsMoving()
    {
        return this.movedInDirection != 0;
    }

    public bool IsGrounded()
    {
        if (this.jumper != null)
            return this.jumper.IsGrounded();
        else
            return true;
    }

    internal void MultiplySpeed(float speedMultiplier)
    {
        this.currentMoveSpeed = this.currentMoveSpeed * speedMultiplier;
    }
}
