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

        public WeaponDataHelper weaponDataHelper;

        public bool isChangeToNewestWeapon = true;
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

        internal int GetCurrentWeaponIndex()
        {
            return CurrentWeaponIndex;
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

        internal WeaponData SelectWeapon(int n)
        {
            if (CurrentWeaponIndex == -1)
                return null;

            if (n < 0 || n >= weaponDataList.Count)
                return null;

            CurrentWeaponIndex = n;

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
            if (!isChangeToNewestWeapon && weaponDataList.Count > 1)
                return true;
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

        public void LoadData(GameData data)
        {
            if (weaponDataHelper == null) return;
            for (int i = 0; i < data.listWeapon.Count; i++)
            {
                Debug.Log("Weapon Name: " + data.listWeapon[i]);
                WeaponData weaponData = weaponDataHelper.GetWeaponData(data.listWeapon[i]);
                if (weaponData != null)
                {
                    Debug.Log("WeaponStorage.LoadData: " + weaponData.weaponName);  
                    AddWeaponData(weaponData);
                }
            }   
        }

        public void SaveData(GameData data)
        {
            if (weaponDataHelper == null) return;
            //for (int i = 0; i < weaponDataList.Count; i++)
            //{
            //    data.listWeapon.Add(weaponDataList[i].weaponName);
            //}

            data.listWeapon.Clear();
            data.listWeapon.AddRange(weaponDataList.Select(w => w.weaponName));
        }
    }
}

