using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Sprite itemSprite;

    public float damage = 1.0f;
    //Implement more weapon stats...

    public virtual void Start()
    {
        WeaponManager.registerWeapon(this);
    }
}
