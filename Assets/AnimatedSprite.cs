using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    //TODO : find way to make this more flexible... interface?
    [SerializeField]
    private PlayerController controller;

    public void OnEndAttack()
    {
        this.controller.OnEndAttack();
    }

    public void Flip(float dir)
    {
        Vector3 flippedScale = this.transform.localScale;
        flippedScale.x = dir;
        this.transform.localScale = flippedScale;
    }
}
