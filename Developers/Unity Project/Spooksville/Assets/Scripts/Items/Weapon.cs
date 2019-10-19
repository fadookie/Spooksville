using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Sprite sprite;

    public float damage = 1.0f;
    //Implement more weapon stats...

    public virtual void Start()
    {
        WeaponManager.registerWeapon(this);
    }

    public void SwapTo()
    {
        //Implement swapping of player weapon
    }
}
