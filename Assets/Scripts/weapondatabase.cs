using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class weapondatabase : ScriptableObject
{
    public weapon[] weapon;

    public int weaponCount
    { get { return weapon.Length; } }

    public weapon Getweapon(int index)
    { return weapon[index]; }
}
