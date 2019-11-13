using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    private int bossHealth = 100;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void Attack(Weapon weapon)
    {
        bossHealth -= weapon.damage;

        //Delay when showing the text before resetting UI
        Invoke("ResetUI", 3);
    }

    private void ResetUI()
    {

    }
}
