using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public Sprite sprite;

    public string WeaponName { get; set; }
    public float Damage { get; set; }

    //Implement more weapon stats...

    public virtual void Initialize()
    {

    }

    public void SwapTo()
    {
        //Implement swapping of player weapon
    }
}
