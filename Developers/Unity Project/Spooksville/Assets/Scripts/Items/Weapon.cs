using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string Name { get; private set; }
    public int Damage
    {
        get
        {
            return Damage;
        }

        private set
        {

        }
    }
    public int ID { get; private set; }
    
    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;

        SetID();

        WeaponManager.instance.RegisterWeapon(this);
    }

    private void SetID()
    {
        ID = new System.Random().Next(int.MaxValue);

        foreach (Weapon weapon in WeaponManager.instance.GetWeapons())
        {
            if (weapon.ID == ID)
            {
                ID = new System.Random().Next(int.MaxValue);
                SetID();
            }
        }
    }
}
