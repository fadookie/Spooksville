using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Inventory
{
    private static List<Weapon> weapons = new List<Weapon>();

    private static bool isPressed;

    private static int window = 1;
    private static int windows = 0;
    private static int row;
    public static int Row
    {
        get
        {
            return row;
        }

        set
        {
            if (value < 0)
            {
                row = 0;

                if (window - 1 < 1)
                {
                    window = 1;
                } else
                {
                    window -= 1;
                }

                return;
            }

            if (value > 2)
            {
                row = 2;

                if (window + 1 > windows)
                {
                    window = windows;
                }
                else
                {
                    window += 1;
                }

                return;
            }

            row = value;
        }
    }
    private static int column;
    public static int Column
    {
        get
        {
            return column;
        }

        set
        {
            if (value < 0)
            {
                column = 0;
                return;
            }

            if (value > 2)
            {
                column = 2;
                return;
            }

            column = value;
        }
    }

    public static void InventorySelection()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if (!isPressed)
        {
            Column += (int)x;
            Row += (int)-y;

            UpdateView();
        }

        if (x != 0 || y != 0)
        {
            isPressed = true;
        }
        else
        {
            isPressed = false;
        }
    }

    public static void UpdateView()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i < 9)
            {
                int weaponIndex = i + (9 * (window - 1));

                if (weapons[i] != null)
                {
                    if (weaponIndex < weapons.Count)
                    {
                        BattleManager.instance.inventoryContainers[i].text = weapons[weaponIndex].Name;
                    } else
                    {
                        BattleManager.instance.inventoryContainers[i].text = "";
                    }

                    BattleManager.instance.inventoryContainers[i].color = new Color(1f, 1f, 1f);
                }
            }
        }

        BattleManager.instance.inventoryContainers[GetPreSelectionWeaponIndex()].color = new Color(0.5f, 1f, 1f);
    }

    public static void Hide()
    {
        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            text.gameObject.SetActive(false);
        }
    }

    public static int GetPreSelectionWeaponIndex()
    {
        //Work on non full 9 screens
        int index = (3 * (Row)) + (Column);
        return index;
    }

    public static void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);

        windows = (weapons.Count / 9) + 1;
    }

    public static List<Weapon> GetInventoryWeapons()
    {
        return weapons;
    }

    public static void Show()
    {
        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            text.gameObject.SetActive(true);
        }
    }
}
 