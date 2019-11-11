using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Inventory
{
    private static List<Weapon> weapons = new List<Weapon>();
    
    public static void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public static List<Weapon> GetInventoryWeapons()
    {
        return weapons;
    }
}
