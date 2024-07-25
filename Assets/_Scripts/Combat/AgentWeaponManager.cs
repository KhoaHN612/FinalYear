using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WeaponSystem
{
    public class AgentWeaponManager : MonoBehaviour
    {
        SpriteRenderer spriteRenderer;

        public WeaponStorage weaponStorage;

        public UnityEvent<Sprite> OnWeaponSwap;
        public UnityEvent OnMultipleWeapons;
        public UnityEvent OnWeaponPickUp;

        private void Awake()
        {
            weaponStorage = GetComponent<WeaponStorage>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            ToggleWeaponVisibility(false);

        }

        public void ToggleWeaponVisibility(bool val)
        {
            if (val)
            {
                SwapWeaponSprite(GetCurrentWeapon().weaponSprite);
            }
            spriteRenderer.enabled = val;
        }

        public WeaponData GetCurrentWeapon()
        {
            return weaponStorage.GetCurrentWeapon();
        }

        private void SwapWeaponSprite(Sprite weaponSprite)
        {
            spriteRenderer.sprite = weaponSprite;
            OnWeaponSwap?.Invoke(weaponSprite);
        }

        public void SwapWeapon()
        {
            if (weaponStorage.WeaponCount <= 0)
                return;
            SwapWeaponSprite(weaponStorage.NextWeapon().weaponSprite);
        }
        public void NextWeapon()
        {
            if (weaponStorage.WeaponCount <= 0)
                return;
            weaponStorage.NextWeapon();
        }
        public void PreviousWeapon()
        {
            if (weaponStorage.WeaponCount <= 0)
                return;
            weaponStorage.PreviousWeapon();
        }

        public void AddWeaponData(WeaponData weaponData)
        {
            if (weaponStorage.AddWeaponData(weaponData) == false)
                return;
            if (weaponStorage.WeaponCount == 2)
                OnMultipleWeapons?.Invoke();
            SwapWeaponSprite(weaponData.weaponSprite);
        }

        public void PickUpWeapon(WeaponData weaponData)
        {
            AddWeaponData(weaponData);
            OnWeaponPickUp?.Invoke();
        }

        public bool CanIUseWeapon(bool isGrounded)
        {
            if (weaponStorage.WeaponCount <= 0)
                return false;
            return weaponStorage.GetCurrentWeapon().CanBeUsed(isGrounded);
        }

        public List<string> GetPlayerWeaponNames()
        {
            return weaponStorage.GetPlayerWeaponNames();
        }

    }
}

