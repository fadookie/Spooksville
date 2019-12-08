using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public static int windows;

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
        Cursor.lockState = CursorLockMode.Locked;

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

    public static void SetInventoryWeapons(List<Weapon> value)
    {
        weapons = value;
    }

    public static void UpdateWindow()
    {
        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            if (text.text != "") return;
        }

        Window -= 1;
    }

    public static void Show()
    {
        Cursor.lockState = CursorLockMode.None;

        IsEnabled = true;

        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            text.gameObject.SetActive(true);
        }
    }
}