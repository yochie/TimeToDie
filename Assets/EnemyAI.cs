using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Mover mover;

    [SerializeField]
    private Collider2D forwardGroundCollider;

    [SerializeField]
    private Collider2D backwardGroundCollider;

    [SerializeField]
    private Collider2D forwardObstacleCollider;

    [SerializeField]
    private Collider2D backwardObstacleCollider;

    [SerializeField]
    private LayerMask groundLayers;

    [SerializeField]
    private LayerMask obstacleLayers;

    [SerializeField]
    private Rigidbody2D rb;


    private float facingDirection;
    private Dictionary<float, Collider2D> groundColliders;
    private Dictionary<float, Collider2D> obstaclColliders;

    // Start is called before the first frame update
    void Start()
    {
        this.facingDirection = 1;
        groundColliders = new();
        groundColliders.Add(1, this.forwardGroundCollider);
        groundColliders.Add(-1, this.backwardGroundCollider);
        obstaclColliders = new();
        obstaclColliders.Add(1, this.forwardObstacleCollider);
        obstaclColliders.Add(-1, this.backwardObstacleCollider);
    }

    // Update is called once per frame
    void Update()
    {
        if(this.CanMoveInDir(this.facingDirection))
            this.mover.MoveInDirection(this.facingDirection);
        else if (this.CanMoveInDir(this.facingDirection * -1))
        {
            this.Flip();
            this.mover.MoveInDirection(this.facingDirection);
        } else
        {
            this.mover.MoveInDirection(0);
        }
    }

    private bool CanMoveInDir(float dir)
    {
        bool thereIsGround = this.groundColliders[dir].IsTouchingLayers(this.groundLayers);
        bool thereIsObstacle = this.obstaclColliders[dir].IsTouchingLayers(this.obstacleLayers);
        return thereIsGround && !thereIsObstacle;
    }

    private void Flip()
    {
        this.facingDirection *= -1;
        this.mover.Flip(this.facingDirection);

    }

    private void FixedUpdate()
    {
        float horizontalVelocity = this.mover.UpdateForFixedFrame();
        float verticalVelocity = this.rb.velocity.y;
        this.rb.velocity = new(horizontalVelocity, verticalVelocity);
        
    }
}
