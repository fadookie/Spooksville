using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            if (weapon.name.Equals(name))
            {
                return weapon;
            }
        }

        return null;
    }

    private void RegisterWeapons()
    {

    }
}
