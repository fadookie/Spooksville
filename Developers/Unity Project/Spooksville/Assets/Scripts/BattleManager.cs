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

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        Inventory.Hide();
    }

    private void Update()
    {
        Inventory.InventorySelection();
    }

    #region Fight Logic
    public void Attack(Weapon weapon)
    {
        bossHealth -= weapon.damage;

        DisplayAttackText("Mom was attacked by " + weapon.name);
    }
    #endregion

    #region UI Management
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

    private IEnumerator WaitSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
