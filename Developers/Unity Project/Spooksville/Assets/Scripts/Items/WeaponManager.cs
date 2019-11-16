using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    private List<Weapon> weapons = new List<Weapon>();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        RegisterWeapons();
        Inventory.AddWeapon(GetWeaponByName("Candy Cane"));
        Inventory.AddWeapon(GetWeaponByName("Driller"));
        Inventory.AddWeapon(GetWeaponByName("A"));
        Inventory.AddWeapon(GetWeaponByName("B"));
        Inventory.AddWeapon(GetWeaponByName("C"));
        Inventory.AddWeapon(GetWeaponByName("D"));
        Inventory.AddWeapon(GetWeaponByName("E"));
        Inventory.AddWeapon(GetWeaponByName("F"));
        Inventory.AddWeapon(GetWeaponByName("G"));
        Inventory.AddWeapon(GetWeaponByName("H"));
    }

    public void RegisterWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public List<Weapon> GetWeapons()
    {
        return weapons;
    }

    public Weapon GetRandomWeapon()
    {
        return weapons[(new System.Random()).Next(weapons.Count)];
    }

    public Weapon GetWeaponByName(string name)
    {
        foreach (Weapon weapon in weapons)
        {
            if (weapon.Name.Equals(name))
            {
                return weapon;
            }
        }

        return null;
    }

    private void RegisterWeapons()
    {
        weapons.Add(new Weapon("Candy Cane", 3));
        weapons.Add(new Weapon("Driller", 200));
        weapons.Add(new Weapon("A", 200));
        weapons.Add(new Weapon("B", 200));
        weapons.Add(new Weapon("C", 200));
        weapons.Add(new Weapon("D", 200));
        weapons.Add(new Weapon("E", 200));
        weapons.Add(new Weapon("F", 200));
        weapons.Add(new Weapon("G", 200));
        weapons.Add(new Weapon("H", 200));
    }
}
