using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField]
    private Rigidbody2D playerRigidbody2D;

    [field: SerializeField]
    public Animator Animator { get; private set; }

    [SerializeField]
    private Mover mover;

    [SerializeField]
    private Jumper jumper;

    private InputAction jumpAction;
    private bool attacking;

    private void Start()
    {
        this.jumpAction = this.playerInput.actions["Jump"];
    }

    private void FixedUpdate()
    {
        //allow player input to control horizontal velocity
        this.mover.UpdateForFixedFrame(jumpHeld: this.jumpAction.IsPressed());

        this.Animator.SetBool("jumping", this.jumper.IsJumping());
        this.Animator.SetBool("grounded", this.jumper.IsGrounded());
        this.Animator.SetBool("running", this.mover.IsMoving());
        this.Animator.SetBool("attacking", this.attacking);
    }

    //Called by input system
    public void OnMove(InputValue value) => this.mover.MoveInDirection(value.Get<Vector2>().x);

    //Called by input system
    public void OnJump(InputValue value)
    {
        this.jumper.Jump();
    }

    //Called by input system
    public void OnFire(InputValue value)
    {
        this.attacking = true;
    }

    public void OnEndAttack()
    {
        this.attacking = false;
    }

    internal void Disable()
    {
        this.enabled = false;
    }
}
