using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WeaponSystem
{
    public class WeaponStorage: MonoBehaviour
    {
        private List<WeaponData> weaponDataList;
        private int currentWeaponIndex;
        [SerializeField]
        private InventoryPlayBarManage inventoryPlayBarManage;
        public int inventorySlotNumber = 4;

        public int WeaponCount { get => weaponDataList.Count; }
        public int CurrentWeaponIndex 
        { 
            get => currentWeaponIndex; 
            set {
                currentWeaponIndex = value;
            }
        }

        private void Awake()
        {
            weaponDataList = new List<WeaponData>();
            if (inventoryPlayBarManage != null)
            {
                inventoryPlayBarManage.Initialization(inventorySlotNumber);
            }
            CurrentWeaponIndex = -1;
        }
        internal WeaponData GetCurrentWeapon()
        {
            if (CurrentWeaponIndex == -1)   
                return null;
            return weaponDataList[CurrentWeaponIndex];
        }

        internal WeaponData NextWeapon()
        {
            if (CurrentWeaponIndex == -1)
                return null;
            CurrentWeaponIndex++;
            if (CurrentWeaponIndex >= weaponDataList.Count)
                CurrentWeaponIndex = 0;
            if (inventoryPlayBarManage != null)
            {
                inventoryPlayBarManage.SelectInventorySlot(currentWeaponIndex);
            }
            return weaponDataList[CurrentWeaponIndex];
        }

        internal WeaponData PreviousWeapon()
        {
            if (CurrentWeaponIndex == -1)
                return null;
            CurrentWeaponIndex--;
            if (CurrentWeaponIndex < 0)
                CurrentWeaponIndex = weaponDataList.Count-1;
            if (inventoryPlayBarManage != null)
            {
                inventoryPlayBarManage.SelectInventorySlot(currentWeaponIndex);
            }
            return weaponDataList[CurrentWeaponIndex];
        }

        internal bool AddWeaponData(WeaponData weaponData)
        {
            if (weaponDataList.Contains(weaponData))
                return false;
            if (weaponDataList.Count >= inventorySlotNumber)
                return false;
            weaponDataList.Add(weaponData);
            if (inventoryPlayBarManage != null)
            {
                int n = inventoryPlayBarManage.TryToAddItem();
                if (n != -1)
                {
                    inventoryPlayBarManage.ReplaceItem(n, weaponData.weaponSprite);
                }
            }
            CurrentWeaponIndex = weaponDataList.Count - 1;
            if (inventoryPlayBarManage != null)
            {
                inventoryPlayBarManage.SelectInventorySlot(currentWeaponIndex);
            }
            return true;
        }

        internal List<string> GetPlayerWeaponNames()
        {
            return weaponDataList.Select(weapon => weapon.name).ToList();
        }
    }
}

