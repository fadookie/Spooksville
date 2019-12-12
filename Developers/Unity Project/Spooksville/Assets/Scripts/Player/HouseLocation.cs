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
                
                for (int i = 0; i < new System.Random().Next(1, 3); i++)
                {
                    Inventory.AddWeapon(WeaponManager.instance.GetRandomWeapon());
                }

                TurnOffHouseLight();
                Destroy(gameObject);
            }
        }
    }

    private void TurnOffHouseLight()
    {
        Collider2D collider = Physics2D.CircleCast(transform.position, 3, Vector2.left, 5f, LayerMask.GetMask("House Lights")).collider;
        if (collider != null) Destroy(collider.gameObject);
    }
}
