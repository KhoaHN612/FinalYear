using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;

public class Damagable : MonoBehaviour, IHittable
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private int maxMana;
    [SerializeField]
    private int currentMana;
    [SerializeField]
    private int maxTime;
    [SerializeField]
    private int currentTime;

    public int CurrentHealth
    {
        get => currentHealth;
        set
        {
            currentHealth = value;
            OnHealthValueChange?.Invoke(currentHealth);

        }
    }
    public int CurrentMana
    {
        get => currentMana;
        set
        {
            if (value >= 0)
            {
                currentMana = value;
            }
            else
            {
                GetHit(Math.Abs(value) * 2);
                currentMana = 0;
            }
            OnManaValueChange?.Invoke(currentMana);

        }
    }
    public int CurrentTime
    {
        get => currentTime;
        set
        {
            if (value >= 0)
            {
                currentTime = value;
            }
            else
            {
                GetHit(Math.Abs(value) * 2);
                currentTime = 0;
            }
            OnTimeValueChange?.Invoke(currentTime);
        }
    }

    public bool CanHit { get; set; } = true;

    public UnityEvent OnGetHit;
    public UnityEvent OnDie;

    public UnityEvent OnAddHealth;
    public UnityEvent<int> OnHealthValueChange;
    public UnityEvent<int> OnInitializeMaxHealth;
    
    public UnityEvent OnAddMana;
    public UnityEvent<int> OnManaValueChange;
    public UnityEvent<int> OnInitializeMaxMana;
    
    public UnityEvent OnAddTime;
    public UnityEvent<int> OnTimeValueChange;

    public UnityEvent<int> OnInitializeMaxTime;

    public void GetHit(GameObject gameObject, int weaponDamage)
    {
        if (CanHit)
            GetHit(weaponDamage);
    }

    public void GetHit(int weaponDamage)
    {
        CurrentHealth -= weaponDamage;
        if (CurrentHealth <= 0)
        {
            OnDie?.Invoke();
        }
        else
        {
            OnGetHit?.Invoke();
        }
    }

    public void AddHealth(int val)
    {
        CurrentHealth = Mathf.Clamp(currentHealth + val, 0, maxHealth);
        OnAddHealth?.Invoke();
    }
    public void AddMana(int val)
    {
        CurrentMana = Mathf.Clamp(currentMana + val, 0, maxMana);
    }
    public void UsesMana(int val)
    {
        CurrentMana = currentMana - val;
    }
    public void AddTime(int val)
    {
        CurrentTime = Mathf.Clamp(currentTime + val, 0, maxTime);
    }
    public void UseTime(int val)
    {
        CurrentTime = currentTime - val;
    }

    public void Initialize(int health)
    {
        maxHealth = health;
        OnInitializeMaxHealth?.Invoke(maxHealth);
        CurrentHealth = maxHealth;
    }
    public void Initialize(int health, int mana, int time)
    {
        maxHealth = health;
        OnInitializeMaxHealth?.Invoke(maxHealth);
        CurrentHealth = maxHealth;

        maxMana = mana;
        OnInitializeMaxMana?.Invoke(maxMana);
        CurrentMana = maxMana;

        maxTime = time;
        OnInitializeMaxTime?.Invoke(maxTime);
        CurrentTime = maxTime;
    }
}
