using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Weapon : MonoBehaviour
{
    public string Name { get; private set; }
    public int Damage { get; private set; }

    public Weapon(string name, int damage)
    {
        Name = name;
        Damage = damage;
    }
}
