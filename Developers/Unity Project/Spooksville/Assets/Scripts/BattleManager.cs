using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Containers")]
    [SerializeField] private Text container;
    private Text previousHeaderText;
    public Text centerInventoryContainer;
    public List<Text> inventoryContainers;

    [Header("Boss Settings")]
    public int bossHealth = 100;

    private List<string> dialog = new List<string>();
    private int dialogIndex;
    private bool isDisplaying;
    private bool isPressed;

    private Coroutine typing;

    private enum BattleState
    {
        Entrance, InventorySelection, Dialogue
    }

    private void Start()
    {
        if (instance == null) instance = this;

        ReadDialogue();
        Inventory.Hide();
    }

    private void Update()
    {
        EntranceDialog();

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
        previousHeaderText.text = container.text;

        container.text = text;
    }

    public void DisplayTemporaryHeaderText(string text, float time)
    {
        container.text = text;
        StartCoroutine(SetToPreviousHeaderText(time));
    }

    private void DisplayAttackText(string text)
    {
        container.text = text;

        Inventory.Hide();
        StartCoroutine(SetToPreviousHeaderText(3f));

        container.text = previousHeaderText.text;
        Inventory.Show();
    }

    private void PreviousHeaderText()
    {
        container.text = previousHeaderText.text;
    }
    #endregion

    #region Dialog Management
    private void EntranceDialog()
    {
        var enter = Input.GetAxisRaw("Select");

        if (enter == 1 && !isPressed)
        {
            isPressed = true;
            isDisplaying = false;

            dialogIndex++;
        }

        if (enter == 0) isPressed = false;

        if (!isDisplaying)
        {
            if (typing != null) StopCoroutine(typing);
            typing = StartCoroutine(Type(container, dialog[dialogIndex], 0.05f));
            isDisplaying = true;
        }
    }

    public IEnumerator Type(Text textContainer, string text, float time)
    {
        textContainer.text = "";

        foreach (char letter in text.ToCharArray())
        {
            textContainer.text += letter;
            yield return new WaitForSeconds(time);

            if (!AudioManager.instance.GetSound("Talking").source.isPlaying)
            {
                AudioManager.instance.Play("Talking");
            }
        }

        if (AudioManager.instance.GetSound("Talking").source.isPlaying)
        {
            AudioManager.instance.Stop("Talking", .3f);
        }
    }

    private void ReadDialogue()
    {
        string path = "Assets/Utility/Dialog/Battle.txt";

        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            dialog.Add(reader.ReadLine());
        }

        reader.Close();
    }
    #endregion

    private IEnumerator SetToPreviousHeaderText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        PreviousHeaderText();
    }
}
