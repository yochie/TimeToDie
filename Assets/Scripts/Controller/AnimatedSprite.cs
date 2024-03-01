using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private EnemyAI enemyController;

    [SerializeField]
    private bool forPlayer;

    [SerializeField]
    private bool defaultFacingRight;

    private IAnimatedSpriteController controller;

    private void Start()
    {
        if (this.forPlayer)
            this.controller = this.playerController;
        else
            this.controller = this.enemyController;

    }

    public void OnEndAttack()
    {
        this.controller.OnEndAttack();
    }

    public void Flip(float dir)
    {
        Vector3 flippedScale = this.transform.localScale;
        flippedScale.x = this.defaultFacingRight ? dir : dir * -1;
        this.transform.localScale = flippedScale;
    }
}
