using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();

    public void Start()
    {
        registerWeapons();

        foreach (Weapon weapon in weapons)
            weapon.Initialize();
    }

    private void registerWeapons()
    {
        weapons.Add(new CandyCaneShooter());
    }
}
