using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem;

public class WeaponDataHelper : MonoBehaviour
{
    [SerializeField] private List<WeaponData> weaponDataList;

    public WeaponData GetWeaponData(string weaponName)
    {
        return weaponDataList.Find(weaponData => weaponData.weaponName == weaponName);
    }       
}
