using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    public Weapon[] weapons;
    private List<Weapon> weaponsList = new List<Weapon>();

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        weaponsList.AddRange(weapons);
    }

    public void RegisterWeapon(Weapon weapon)
    {
        weaponsList.Add(weapon);
    }

    public List<Weapon> GetWeapons()
    {
        return weaponsList;
    }

    public Weapon GetRandomWeapon()
    {
        return weaponsList[(new System.Random()).Next(weaponsList.Count)];
    }

    public Weapon GetWeaponByName(string name)
    {
        foreach (Weapon weapon in weaponsList)
        {
            if (weapon.name.Equals(name))
            {
                return weapon;
            }
        }

        return null;
    }
}
