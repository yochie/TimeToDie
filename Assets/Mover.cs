using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [field: SerializeField]
    public Collider2D Collider { get; private set; }

    [field: SerializeField]
    public int DefaultMoveSpeed { get; private set; }

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public int CurrentMoveSpeed { get; set; }

    //for flipping sprite whenever changing dir
    private float previousDirection;

    private float movedInDirection;

    private void Start()
    {
        this.CurrentMoveSpeed = this.DefaultMoveSpeed;
        this.movedInDirection = 0;
        this.previousDirection = 0;
    }

    public float UpdateForFrame()
    {
        //flip sprite
        if (this.movedInDirection != 0 && this.movedInDirection != this.previousDirection)
        {
            this.Flip(faceDir: this.movedInDirection);
        }
        this.previousDirection = this.movedInDirection;

        return this.CurrentMoveSpeed * this.movedInDirection;
    }

    private void Flip(float faceDir)
    {
        this.spriteRenderer.flipX = faceDir == 1 ? false : true;
    }

    public void MoveInDirection(float direction)
    {
        this.movedInDirection = direction;
    }

    public bool IsMoving()
    {
        return this.movedInDirection != 0;
    }

}
