using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public DisplayBarUI healthBar, manaBar, timeBar;

    public void InitializeMaxHealth(int maxHealth)
    {
        healthBar.Initialize(maxHealth);
    }
    public void InitializeMaxMana(int maxMana)
    {
        manaBar.Initialize(maxMana);
    }
    public void InitializeMaxTime(int maxTime)
    {
        timeBar.Initialize(maxTime);
    }
    public void SetHealth(int health){
        healthBar.SetDisplay(health);
    }
    public void SetMana(int mana){
        manaBar.SetDisplay(mana);
    }
    public void SetTime(int time){
        timeBar.SetDisplay(time);
    }
}
