using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerWeaponController : MonoBehaviour
{
    private static Weapon currentWeapon;
    //Create child weapon prefab

    public static void SetWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }
}
