using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private Health health;

    [SerializeField]
    private float maxPain;

    [SerializeField]
    private UIController ui;

    [SerializeField]
    private GameController gameController;

    [SerializeField]
    private PlayerController playerController;

    private float currentPain;

    public bool CanTakeDamage { get; private set; }

    private void Start()
    {        
        this.currentPain = 0;
        this.CanTakeDamage = true;
        this.health.Init();
        this.ui.SetHealthBar(this.health.CurrentHealth, this.health.MaxHealth);
        this.ui.SetPain(this.currentPain, this.maxPain);
    }

    //will apply damage and then pain if character survives hit
    public void Hurt(float damageAmount, float painAmount)
    {
        this.health.CurrentHealth -= damageAmount;
        //update view
        this.ui.SetHealthBar(this.health.CurrentHealth, this.health.MaxHealth);
        this.ui.TriggerDamageFlash();
        this.ui.TriggerScreenShake();

        if (this.health.CurrentHealth <= 0)
        {
            StartCoroutine(this.EndGame(died: true)); 
            return;
        }

        //Check for pain after death so that killing blow deals no damage
        this.currentPain = Mathf.Clamp(this.currentPain + painAmount, 0, this.maxPain);
        this.ui.SetPain(this.currentPain, this.maxPain);

        if (this.currentPain >= this.maxPain)
        {
            StartCoroutine(this.EndGame(died: false));            
        }
    }

    private IEnumerator EndGame(bool died)
    {
        this.playerController.Disable();
        this.CanTakeDamage = false;
        this.playerController.Animator.SetBool("dead", true);
        this.gameController.StopTimer();
        yield return new WaitForSeconds(2);
        this.gameController.EndGame(died: died, this.currentPain, this.maxPain);
    }
}
