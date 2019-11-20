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

    private static float dirX;
    private static float dirY;

    private static int row;
    private static int totalRows;
    private static int exactRow;
    private static int ExactRow
    {
        get
        {
            return exactRow;
        }

        set
        {

        }
    }
    public static int ViewRow
    {
        get
        {
            return row;
        }

        set
        {
            if (value < 0)
            {
                if (window - 1 < 1)
                {
                    row = 0;
                    window = 1;
                } else
                {
                    row = 2;
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
                    row = 0;
                }

                return;
            }

            if (GetPreSelectionWeaponIndex() + 3 <= 8)
            {
                if (dirY < 0 && BattleManager.instance.inventoryContainers[GetPreSelectionWeaponIndex() + 3].text == "") return;
            }

            row = value;
        }
    }

    private static int column;
    private static int exactColumn;
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

            if (GetPreSelectionWeaponIndex() + 1 > 2)
            {
                if (dirX > 0 && BattleManager.instance.inventoryContainers[GetPreSelectionWeaponIndex() + 1].text == "") return;
            }

            column = value;
        }
    }

    public static bool IsEnabled { get; private set; }

    public static void InventorySelection()
    {
        Debug.Log(window + " | " + windows);

        dirX = Input.GetAxisRaw("Horizontal");
        dirY = Input.GetAxisRaw("Vertical");

        if (!isPressed)
        {
            Column += (int)dirX;
            ViewRow += (int)-dirY;
        }

        UpdateView();

        if (dirX != 0 || dirY != 0)
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
                        BattleManager.instance.inventoryContainers[i].text = weapons[weaponIndex].name;
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
        IsEnabled = false;

        foreach (Text text in BattleManager.instance.inventoryContainers)
        {
            text.gameObject.SetActive(false);
        }
    }

    public static int GetPreSelectionWeaponIndex()
    {
        //Dont RESET row / column #
        //Work on non full 9 screens
        int index = (3 * (ViewRow)) + (Column);
        return index;
    }

    public static void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);

        windows = (weapons.Count / 9);
    }

    public static List<Weapon> GetInventoryWeapons()
    {
        return weapons;
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
 