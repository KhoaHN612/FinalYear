using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public DisplayBarUI healthBar, manaBar, timeBar;
    public UnityEvent healthChange, manaChange, timeChange;

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
        healthChange?.Invoke();
        healthBar.SetDisplay(health);
    }
    public void SetMana(int mana){
        manaChange?.Invoke();
        manaBar.SetDisplay(mana);
    }
    public void SetTime(int time){
        timeChange?.Invoke();
        timeBar.SetDisplay(time);
    }
}
