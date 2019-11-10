using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLocation : MonoBehaviour
{
    private bool isTriggered = false;

    void Start()
    {

    }

    void Update()
    {
        
    }
    //the private things
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            if (!isTriggered)
            {
                Debug.Log("U got a weapon!");
                isTriggered = true;
                WeaponManager.instance.GetRandomWeapon();
            }
        }
    }
}
