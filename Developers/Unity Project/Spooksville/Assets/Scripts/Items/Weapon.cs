using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public Sprite sprite;

    public string name;
    public float damage;

    //Implement more weapon stats...

    public virtual void Initialize()
    {
        name = null;
        damage = 1.0f;
    }
}
