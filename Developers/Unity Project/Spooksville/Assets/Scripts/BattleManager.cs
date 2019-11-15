using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Containers")]
    [SerializeField] private Text headerContainer;
    private Text previousHeaderText;
    public Text centerInventoryContainer;
    public List<Text> inventoryContainers;

    [Header("Boss Settings")]
    public int bossHealth = 100;

    private string weaponSelectionMessage = "What weapon will you choose?";

    private float selectionDelay = 0.325f;
    private float lastSelection;
    private bool isPressed;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Inventory.Show();
    }

    private void Update()
    {
        InventorySelection();
    }

    #region Fight Logic
    public void Attack(Weapon weapon)
    {
        bossHealth -= weapon.Damage;

        DisplayAttackText("Mom was attacked by " + weapon.Name);
    }
    #endregion

    #region UI Management
    #region Text Display
    public void DisplayHeaderText(string text)
    {
        previousHeaderText.text = headerContainer.text;

        headerContainer.text = text;
    }

    public void DisplayTemporaryHeaderText(string text, float time)
    {
        headerContainer.text = text;
        StartCoroutine(WaitSeconds(time));
        PreviousHeaderText();
    }

    private void DisplayAttackText(string text)
    {
        headerContainer.text = text;

        Inventory.Hide();
        StartCoroutine(WaitSeconds(3f));

        headerContainer.text = previousHeaderText.text;
        Inventory.Show();
    }

    private void PreviousHeaderText()
    {
        headerContainer.text = previousHeaderText.text;
    }
    #endregion

    #region Inventory Selection
    private void InventorySelection()
    {
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        if (!isPressed)
        {
            Inventory.Column += (int)x;
            Inventory.Row += (int)-y;

            Inventory.UpdateView();
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
    #endregion

    #endregion

    private IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
