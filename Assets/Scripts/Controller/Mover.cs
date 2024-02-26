using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [field: SerializeField]
    public Collider2D Collider { get; private set; }

    [field: SerializeField]
    public float DefaultMoveSpeed { get; private set; }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Jumper jumper;

    [SerializeField]
    private Rigidbody2D rb;

    private float currentMoveSpeed;

    //for flipping sprite whenever changing dir
    private float previousDirection;

    private float movedInDirection;

    private float knockbackRemainingSeconds;

    private void Start()
    {
        this.currentMoveSpeed = this.DefaultMoveSpeed;
        this.movedInDirection = 0;
        this.previousDirection = 0;
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

        if (knockbackRemainingSeconds > 0)
        {
            //let physics handle velocity during knockback
            this.knockbackRemainingSeconds -= Time.fixedDeltaTime;
        }
        else
        {
            //manually update velocity based on input
            Vector2 previousVelocity = this.rb.velocity;
            float xVelocity = this.currentMoveSpeed * this.movedInDirection;
            float yVelocity;
            if (this.jumper != null)
                yVelocity = this.jumper.UpdateForFrame(previousVelocity.y, jumpHeld);
            else
                yVelocity = previousVelocity.y;
            this.rb.velocity = new Vector2(xVelocity, yVelocity);
        }
    }

    public void Flip(float faceDir)
    {
        this.spriteRenderer.flipX = faceDir == 1 ? false : true;
        this.previousDirection = faceDir;
    }

    public void MoveInDirection(float direction)
    {
        this.movedInDirection = direction;
    }

    public void Knockback(float durationSeconds, Vector2 knockbackVelocity)
    {
        this.knockbackRemainingSeconds = durationSeconds;
        this.rb.velocity = knockbackVelocity;
    }

    public bool IsMoving()
    {
        return this.movedInDirection != 0;
    }

    internal void MultiplySpeed(float speedMultiplier)
    {
        this.currentMoveSpeed = this.currentMoveSpeed * speedMultiplier;
    }
}
