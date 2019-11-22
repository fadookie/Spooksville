using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseLocation : MonoBehaviour
{
    private bool hasTriggered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasTriggered)
        {
            if (collision.gameObject.tag.Equals("Player"))
            {
                hasTriggered = true;
                DialogManager.instance.DisplayRandomCollectText();
                Inventory.AddWeapon(WeaponManager.instance.GetRandomWeapon());
                Destroy(gameObject);
            }
        }
    }
}
