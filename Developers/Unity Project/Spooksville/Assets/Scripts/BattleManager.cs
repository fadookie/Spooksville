using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Containers")]
    [SerializeField] private Text charachterDialog;
    [SerializeField] private Text headerContainer;

    private Text previousHeaderText;
    public Text centerInventoryContainer;
    public List<Text> inventoryContainers;

    [Header("Boss Settings")]
    public int bossHealth = 100;

    private List<string> dialog = new List<string>();
    private int dialogIndex;
    private bool isDisplaying;
    private bool isPressed;

    private bool isInBattle;

    private Coroutine typing;

    private void Start()
    {
        if (instance == null) instance = this;

        headerContainer.text = "";

        ReadDialogue();
        Inventory.Hide();
    }

    private void Update()
    {
        EntranceDialog();

        Inventory.InventorySelection();
    }

    #region Fight Logic
    private void StartBattle()
    {
        isInBattle = true;

        StopCoroutine(typing);
        charachterDialog.gameObject.SetActive(false);
        GameObject.Find("Text Canvas").transform.Find("Enter to Continue").gameObject.SetActive(false);
        if (AudioManager.instance.GetSound("Talking").source.isPlaying) AudioManager.instance.Stop("Talking", .3f);

        headerContainer.text = "What weapon will you choose?";

        Inventory.Show();
    }

    public void Attack(Weapon weapon)
    {
        bossHealth -= weapon.damage;

        DisplayAttackText("Mom was attacked by " + weapon.name);
    }
    #endregion

    #region UI Management
    public void DisplayHeaderText(string text)
    {
        previousHeaderText.text = charachterDialog.text;

        charachterDialog.text = text;
    }

    public void DisplayTemporaryHeaderText(string text, float time)
    {
        charachterDialog.text = text;
        StartCoroutine(SetToPreviousHeaderText(time));
    }

    private void DisplayAttackText(string text)
    {
        charachterDialog.text = text;

        Inventory.Hide();
        StartCoroutine(SetToPreviousHeaderText(3f));

        charachterDialog.text = previousHeaderText.text;
        Inventory.Show();
    }

    private void PreviousHeaderText()
    {
        charachterDialog.text = previousHeaderText.text;
    }
    #endregion

    #region Dialog Management
    private void EntranceDialog()
    {
        if (!isInBattle)
        {
            var enter = Input.GetAxisRaw("Select");

            if (enter == 1 && !isPressed)
            {
                isPressed = true;
                isDisplaying = false;

                if (dialogIndex + 1 == dialog.Count)
                {
                    StartBattle();
                    return;
                }
                else
                {
                    dialogIndex++;
                }
            }

            if (enter == 0) isPressed = false;

            if (!isDisplaying)
            {
                if (typing != null) StopCoroutine(typing);
                typing = StartCoroutine(Type(charachterDialog, dialog[dialogIndex], 0.05f));
                isDisplaying = true;
            }
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

        if (!AudioManager.instance.GetSound("Talking").source.isPlaying) AudioManager.instance.Play("Talking");

        AudioManager.instance.Stop("Talking", .725f);
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
