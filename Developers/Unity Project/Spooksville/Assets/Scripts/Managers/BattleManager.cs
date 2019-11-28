using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Containers")]
    public Text charachterDialog;

    public Text headerContainer;

    [Header("UI Objects")]
    public Text centerInventoryContainer;

    public List<Text> inventoryContainers;

    [Header("Boss Settings")]
    public int bossHealth = 100;

    [Header("Other")]
    public float attackTextDuration;

    private List<string> entranceDialog = new List<string>();
    private List<string> battleDialog = new List<string>();
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

        StartBattle();
        Cursor.lockState = CursorLockMode.None;

        AudioManager.instance.Play("Boss Theme", true);
    }

    private void Update()
    {

    }

    #region Fight Logic

    private void StartBattle()
    {
        Inventory.UpdateView();

        isInBattle = true;
        canAttack = true;

        charachterDialog.gameObject.SetActive(false);
        GameObject.Find("Text Canvas").transform.Find("Enter to Continue").gameObject.SetActive(false);
        if (AudioManager.instance.GetSound("Talking").source.isPlaying) AudioManager.instance.Stop("Talking", .3f);

        headerContainer.text = "What weapon will you choose?";

        Inventory.Show();
    }

    public void Attack(Weapon weapon)
    {
        bossHealth -= weapon.damage;

        DisplayAttackText(weapon);
    }

    #endregion Fight Logic

    #region UI Management

    public void OnScroll(int direction)
    {
        if (!canAttack) return;

        Inventory.Window += direction;
        Inventory.UpdateView();
    }

    public void OnSelect(int textSlot)
    {
        if (!canAttack) return;

        Weapon weapon = Inventory.GetInventoryWeapons().Find(w => w.name == inventoryContainers[textSlot].text);
        if (weapon != null) Attack(weapon);
    }

    private void DisplayAttackText(Weapon weapon)
    {
        headerContainer.text = "Mom was attacked by a " + weapon.name;

        Inventory.Hide();

        StartCoroutine(ResetToAttackState(weapon, attackTextDuration));
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

                if (dialogIndex + 1 == entranceDialog.Count)
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
                typing = StartCoroutine(Type(charachterDialog, entranceDialog[dialogIndex], 0.05f));
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
        TextAsset txt = (TextAsset)Resources.Load("BattleEntrance");
        string fixedText = txt.text.Replace(System.Environment.NewLine, "");
        foreach (string log in fixedText.Split('/')) entranceDialog.Add(log);

        txt = (TextAsset)Resources.Load("Mom");
        fixedText = txt.text.Replace(System.Environment.NewLine, "");
        foreach (string log in fixedText.Split('/')) battleDialog.Add(log);
    }

    #endregion Dialog Management

    private IEnumerator ResetToAttackState(Weapon weapon, float seconds)
    {
        canAttack = false;
        Inventory.RemoveWeapon(weapon);
        Inventory.UpdateView();

        yield return new WaitForSeconds(seconds);

        string txt = battleDialog[(new System.Random()).Next(battleDialog.Count)];
        var time = 0f;

        float messageSpeed = 0.02f;

        foreach (char c in txt.ToCharArray()) time += messageSpeed;

        StartCoroutine(Type(headerContainer, txt, messageSpeed));

        yield return new WaitForSeconds(time + 2f);

        Inventory.Show();
        Inventory.UpdateWindow();
        Inventory.UpdateView();

        canAttack = true;
        headerContainer.text = "What will you use against your Mom?";
    }
}