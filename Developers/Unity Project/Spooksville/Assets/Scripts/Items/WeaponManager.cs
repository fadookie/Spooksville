using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponManager : MonoBehaviour
{
    private static List<Weapon> weapons = new List<Weapon>();

    public static void registerWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }
}
