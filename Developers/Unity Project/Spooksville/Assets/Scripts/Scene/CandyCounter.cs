using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CandyCounter : MonoBehaviour
{
    private void Update()
    {
        GetComponent<Text>().text = "x " + Inventory.GetInventoryWeapons().Count;
    }
}
