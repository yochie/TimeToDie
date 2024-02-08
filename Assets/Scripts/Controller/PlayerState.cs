using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int maxPain;

    [SerializeField]
    private UIController ui;

    [SerializeField]
    private GameController gameController;

    private int currentPain;
    private int currentHealth;

    private void Start()
    {
        this.currentHealth = this.maxHealth;
        this.currentPain = 0;
    }

    //will apply damage and then pain if character survives hit
    public void Hurt(int damageAmount, int painAmount)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth - damageAmount, 0, this.maxHealth);
        //update view
        this.ui.SetHealth(this.currentHealth, this.maxHealth);

        if (this.currentHealth <= 0)
        {
            this.gameController.EndGame(died: true, this.currentPain);
            return;
        }

        //Check for pain after death so that killing blow deals no damage
        this.currentPain = Mathf.Clamp(this.currentPain + painAmount, 0, this.maxPain);
        this.ui.SetPain(this.currentPain, this.maxPain);

        if (this.currentPain >= this.maxPain)
        {
            this.gameController.EndGame(died: false, this.currentPain);
        }

    }

    public (int, int) GetHealthStats()
    {
        return (this.currentHealth, this.maxHealth);
    }

    public (int, int) GetPainStats()
    {
        return (this.currentPain, this.maxPain);
    }
}
