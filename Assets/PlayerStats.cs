using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int maxPain;

    [SerializeField]
    private StatBar healthBar;

    [SerializeField]
    private StatBar painBar;

    private int currentPain;
    private int currentHealth;

    private void Start()
    {
        this.currentHealth = this.maxHealth;
        this.currentPain = 0;
    }

    public void TakeDamage(int damageAmount)
    {
        this.currentHealth = Mathf.Clamp(this.currentHealth - damageAmount, 0, this.maxHealth);
        //update view
        this.healthBar.SetVal(this.currentHealth, this.maxHealth);
    }

    public void TakePain(int painAmount) 
    {
        this.currentPain = Mathf.Clamp(this.currentPain + painAmount, 0, this.maxPain);
        //update view        
        this.painBar.SetVal(this.currentPain, this.maxPain);
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
