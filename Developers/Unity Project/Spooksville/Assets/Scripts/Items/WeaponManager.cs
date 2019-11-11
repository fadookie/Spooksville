using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    private List<Weapon> weapons = new List<Weapon>();

    public Weapon GetRandomWeapon()
    {
        return weapons[Mathf.RoundToInt(Random.Range(0, weapons.Count - 1))];
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        RegisterWeapons();

        foreach (Weapon weapon in weapons)
            weapon.Initialize();
    }

    private void RegisterWeapons()
    {
        weapons.Add(new CandyCaneShooter());
    }
}
