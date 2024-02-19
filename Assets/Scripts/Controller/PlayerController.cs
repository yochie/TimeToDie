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

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Mover mover;

    [SerializeField]
    private Jumper jumper;

    private InputAction jumpAction;

    private void Start()
    {
        this.jumpAction = this.playerInput.actions["Jump"];
    }

    private void FixedUpdate()
    {
        //allow player input to control horizontal velocity
        float xVelocity = this.mover.UpdateForFrame();

        //let physics handle vertical velocity unless jumping
        float yVelocity = this.jumper.UpdateForFrame(this.playerRigidbody2D.velocity.y, jumpHeld: this.jumpAction.IsPressed());

        //set velocity for this frame based on mover and jumper updates
        this.playerRigidbody2D.velocity = new Vector2(xVelocity, yVelocity);

        this.animator.SetBool("jumping", this.jumper.IsJumping());
        this.animator.SetBool("grounded", this.jumper.IsGrounded());
        this.animator.SetBool("running", this.mover.IsMoving());
    }

    //Called by input system
    public void OnMove(InputValue value) => this.mover.MoveInDirection(value.Get<Vector2>().x);

    //Called by input system
    public void OnJump(InputValue value)
    {
        this.jumper.Jump();
    }
}
