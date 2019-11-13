using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon
{
    public Sprite sprite;

    public string name;
    public int damage;

    public virtual void Initialize()
    {
        name = null;
        damage = 1;
    }
}
