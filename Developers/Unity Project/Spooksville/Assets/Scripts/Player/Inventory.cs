using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class Inventory
{
    private static List<Weapon> weapons = new List<Weapon>();

    private static bool isPressed;

    private static int window = 1;
    private static int windows = 0;

    public static bool IsEnabled { get; private set; }

    public static void UpdateView()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            int weaponIndex = i + (9 * (window - 1));

            if (i < 9)
            {
                if (weapons[i] != null)
                {
                    if (weaponIndex < weapons.Count)
                    {
                        BattleManager.instance.inventoryContainers[i].text = weapons[weaponIndex].name;
                    }
                    else
                    {
                        BattleManager.instance.inventoryContainers[i].text = "";
                    }
                }
            }
        }
    }

    public static void Hide()
    {
        IsEnabled = false;

        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            text.gameObject.SetActive(false);
        }
    }

    public static void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);

        windows = (weapons.Count / 9);
        if (weapons.Count % 9 != 0) windows++;
    }

    public static void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);

        windows = (weapons.Count / 9);
        if (weapons.Count % 9 != 0) windows++;
    }

    public static List<Weapon> GetInventoryWeapons()
    {
        return weapons;
    }

    public static void UpdateWindow()
    {
        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            if (text.text != "") return;
        }

        if (window != 1) window -= 1;
    }

    public static void Show()
    {
        IsEnabled = true;

        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            text.gameObject.SetActive(true);
        }
    }
}