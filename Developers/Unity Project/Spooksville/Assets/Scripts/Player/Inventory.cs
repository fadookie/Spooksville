using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public abstract class Inventory
{
    private static List<Weapon> weapons = new List<Weapon>();

    public static bool IsViewable { get; private set; }

    private static int window;
    private static int maxWindows;

    private static bool isPressed;

    private static int row;

    public static int Row { get; private set; }

    private static int column;

    public static int Column { get; private set; }

    public static void Show()
    {
        foreach (Text text in BattleManager.instance.inventoryContainers) text.gameObject.SetActive(true);
        IsViewable = true;
    }

    public static void InventorySelection()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if (!isPressed)
        {
            Column += (int)x;
            Row += (int)-y;
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

    }

    public static void Hide()
    {
        foreach (Text text in BattleManager.instance.inventoryContainers) text.gameObject.SetActive(false);
        IsViewable = false;
    }

    public static void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);

        maxWindows = weapons.Count / 9;
        if (maxWindows % 9 != 0) maxWindows++;
    }

    private static void DisplayWindow(int window)
    {

    }
}