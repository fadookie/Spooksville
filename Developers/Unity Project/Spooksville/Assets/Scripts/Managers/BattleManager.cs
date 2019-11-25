using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
    [HideInInspector] public bool canAttack;

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
    }

    #region Fight Logic

    private void StartBattle()
    {
        Inventory.UpdateView();

        isInBattle = true;
        canAttack = true;

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
        Debug.Log(weapon.damage);

        DisplayAttackText(weapon);
    }

    #endregion Fight Logic

    #region UI Management

    public void DisplayHeaderText(string text)
    {
        previousHeaderText.text = charachterDialog.text;

        charachterDialog.text = text;
    }

    private void DisplayAttackText(Weapon weapon)
    {
        headerContainer.text = "Mom was attacked by a " + weapon.name;

        Inventory.Hide();

        StartCoroutine(ResetToAttackState(weapon, .5f));
    }

    #endregion UI Management

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

    #endregion Dialog Management

    public void OnSelect(int textSlot)
    {
        if (!canAttack) return;

        Attack(Inventory.GetInventoryWeapons().Find(w => w.name == inventoryContainers[textSlot].text));
    }

    private IEnumerator ResetToAttackState(Weapon weapon, float seconds)
    {
        canAttack = false;

        yield return new WaitForSeconds(seconds);

        Inventory.Show();

        canAttack = true;
        headerContainer.text = "What weapon will you choose?";
    }
}