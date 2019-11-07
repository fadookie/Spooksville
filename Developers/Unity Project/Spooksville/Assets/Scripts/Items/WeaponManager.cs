using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;
    private List<Weapon> weapons = new List<Weapon>();

    public Weapon GetRandomWeapon()
    {
        Mathf.RoundToInt(Random.Range(0, weapons.Count - 1));
    }

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        RegisterWeapons();

        foreach (Weapon weapon in weapons)
            weapon.Initialize();
    }

    private void RegisterWeapons()
    {
        weapons.Add(new CandyCaneShooter());
    }
}
