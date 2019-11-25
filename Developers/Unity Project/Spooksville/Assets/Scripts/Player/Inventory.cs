using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class Inventory
{
    private static List<Weapon> weapons = new List<Weapon>();

    private static int window;
    public static int Window
    {
        get
        {
            return window;
        }

        set
        {
            if (value < 0 || value > windows - 1) return;

            window = value;
        }
    }
    private static int windows;

    public static bool IsEnabled { get; private set; }

    public static void UpdateView()
    {
        for (int i = 0; i < 9; i++)
        {
            int weaponIndex = i + (9 * (window));

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
        if (weapons.Count % 9 != 0 && weapons.Count > 9) windows++;
    }

    public static void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);

        windows = (weapons.Count / 9);
        if (weapons.Count % 9 != 0 && weapons.Count > 9) windows++;
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

        Debug.Log("No error");
        Window -= 1;
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