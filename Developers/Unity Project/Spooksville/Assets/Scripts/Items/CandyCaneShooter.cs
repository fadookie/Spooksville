using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyCaneShooter : Weapon
{
    public override void Initialize()
    {
        base.Initialize();

        name = "Candy Cane";
        damage = 3.0f;
    }
}
