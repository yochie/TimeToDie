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

    private float currentMoveSpeed;

    //for flipping sprite whenever changing dir
    private float previousDirection;

    private float movedInDirection;

    private float knockbackRemainingSeconds;
    private float knockbackDirection;
    private float knockbackSpeed;

    private void Start()
    {
        this.currentMoveSpeed = this.DefaultMoveSpeed;
        this.movedInDirection = 0;
        this.previousDirection = 0;
    }

    //updates tracked state and returns velocity for frame
    public float UpdateForFixedFrame()
    {
        //flip sprite
        if (this.movedInDirection != 0 && this.movedInDirection != this.previousDirection)
        {
            this.Flip(faceDir: this.movedInDirection);
        }
        this.previousDirection = this.movedInDirection;

        if(knockbackRemainingSeconds > 0)
        {
            this.knockbackRemainingSeconds -= Time.fixedDeltaTime;
            return this.knockbackSpeed * this.knockbackDirection;
        } else 
            return this.currentMoveSpeed * this.movedInDirection;
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

    public void Knockback(float durationSeconds, float dir, float speed)
    {
        this.knockbackRemainingSeconds = durationSeconds;
        this.knockbackDirection = dir;
        this.knockbackSpeed = speed;
        Debug.Log(knockbackDirection);
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
