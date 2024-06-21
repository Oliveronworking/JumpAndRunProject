using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponDatabase : ScriptableObject
{
    public Weapon[] weapons;

    public int weaponCount
    {
        get { return weapons.Length; }
    }

    public Weapon GetWeapon(int index)
    {
        return weapons[index];
    }
}
