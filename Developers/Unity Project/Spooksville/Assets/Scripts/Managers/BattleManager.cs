using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Boss Settings")]
    [SerializeField] private int bossHealth;

    public int BossHealth
    {
        get
        {
            return bossHealth;
        }

        set
        {
            if (value <= 0)
            {
                bossHealth = 0;
                EndBattle();
                return;
            }

            bossHealth = value;
        }
    }

    [Header("Text Objects")]
    [Header("UI Objects")]
    public Text headerContainer;
    public List<Text> inventoryContainers;
    public Text bossHP;
    public Text candyAmount;

    [Header("Animated Objects")]
    public Image mom;
    public Image player;
    public Image background;

    [Header("Other")]
    public float attackTextDuration;

    private List<string> entranceDialog;
    private List<string> battleDialog;

    [HideInInspector] public bool canAttack;

    [HideInInspector] public GameObject leftArrow;
    [HideInInspector] public GameObject rightArrow;

    private bool isEndingBattle;
    private Coroutine attackResetState;

    private void Start()
    {
        leftArrow = GameObject.Find("Canvas UI").transform.Find("Down Scroll").gameObject;
        rightArrow = GameObject.Find("Canvas UI").transform.Find("Up Scroll").gameObject;

        if (instance == null) instance = this;

        headerContainer.text = "";

        ReadDialogue();
        Inventory.Hide();

        StartBattle();
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        Debug.Log(Inventory.Window + " | " + Inventory.windows);

        if (bossHealth <= 100)
        {
            background.gameObject.GetComponent<Animator>().SetTrigger("Activate");
        }

        #region Inventory Arrow Display
        if (!Inventory.IsEnabled)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
            return;
        }

        if (Inventory.windows == 1)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }
        else if (Inventory.Window == 0)
        {
            leftArrow.SetActive(false);
            rightArrow.SetActive(true);
        }
        else if (Inventory.Window == Inventory.windows - 1)
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(false);
        }
        else
        {
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
        #endregion
    }

    #region Fight Logic

    private void StartBattle()
    {
        CheckInventory();

        candyAmount.text = "x " + Inventory.GetInventoryWeapons().Count.ToString();
        bossHP.text = "HP ♥ " + bossHealth.ToString();

        Inventory.UpdateView();

        canAttack = true;

        headerContainer.text = "What weapon will you choose?";

        Inventory.Show();
    }

    private void EndBattle()
    {
        if (!isEndingBattle)
        {
            isEndingBattle = true;
            FadeAnimation.instance.LoadScene(5);
        }
    }

    public void Attack(Weapon weapon)
    {
        DisplayAttackText(weapon);

        StartCoroutine(DrainHealth(weapon.damage));

        mom.gameObject.GetComponent<Animator>().SetTrigger("Activate");
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

        attackResetState = StartCoroutine(ResetToAttackState(weapon, attackTextDuration));
    }

    #endregion UI Management

    #region Dialog Management

    public IEnumerator Type(Text textContainer, string text, float time)
    {
        textContainer.text = "";

        foreach (char letter in text.ToCharArray())
        {
            textContainer.text += letter;

            yield return new WaitForSeconds(time);

            if (!AudioManager.instance.GetSound("Talking").source.isPlaying && !GameManager.instance.IsPaused)
            {
                AudioManager.instance.Play("Talking");
            }
        }

        if (!GameManager.instance.IsPaused)
        {
            if (!AudioManager.instance.GetSound("Talking").source.isPlaying) AudioManager.instance.Play("Talking");

            AudioManager.instance.Stop("Talking", 0.05f);
        }
    }

    public IEnumerator BossEndMessage(Text textContainer, string text, float time)
    {
        textContainer.text = "";

        foreach (char letter in text.ToCharArray())
        {
            textContainer.text += letter;

            yield return new WaitForSeconds(time);

            if (!AudioManager.instance.GetSound("Talking").source.isPlaying && !GameManager.instance.IsPaused)
            {
                AudioManager.instance.Play("Talking");
            }
        }

        if (!GameManager.instance.IsPaused)
        {
            if (!AudioManager.instance.GetSound("Talking").source.isPlaying) AudioManager.instance.Play("Talking");

            AudioManager.instance.Stop("Talking", 0.05f);
        }

        yield return new WaitForSeconds(attackTextDuration);

        PauseMenu.TriggerGameOver(GameManager.instance.endingMessages[new System.Random().Next(GameManager.instance.endingMessages.Count)]);
    }

    private void ReadDialogue()
    {
        entranceDialog = new List<string>();
        battleDialog = new List<string>();

        TextAsset txt = (TextAsset)Resources.Load("BattleEntrance");
        string fixedText = txt.text.Replace("\n", string.Empty);
        foreach (string log in fixedText.Split('/')) entranceDialog.Add(log);

        txt = (TextAsset)Resources.Load("Mom");
        fixedText = txt.text.Replace("\n", string.Empty);
        foreach (string log in fixedText.Split('/')) battleDialog.Add(log);
    }

    #endregion Dialog Management

    private IEnumerator ResetToAttackState(Weapon weapon, float seconds)
    {
        canAttack = false;
        Inventory.RemoveWeapon(weapon);
        Inventory.UpdateView();

        candyAmount.text = "x " + Inventory.GetInventoryWeapons().Count.ToString();

        yield return new WaitForSeconds(seconds);

        string txt = battleDialog[new System.Random().Next(battleDialog.Count)];
        var time = 0f;

        float messageSpeed = 0.02f;

        foreach (char c in txt.ToCharArray()) time += messageSpeed;

        StartCoroutine(Type(headerContainer, txt, messageSpeed));

        yield return new WaitForSeconds(time + attackTextDuration);

        txt = "Give me some of your candy!";
        time = 0f;

        foreach (char c in txt.ToCharArray()) time += messageSpeed;

        player.gameObject.GetComponent<Animator>().SetTrigger("Activate");

        StartCoroutine(Type(headerContainer, txt, messageSpeed));

        var candyDifference = new System.Random().Next(1, 5);
        var prevCount = Inventory.GetInventoryWeapons().Count;

        StartCoroutine(DrainCandy(candyDifference));

        yield return new WaitForSeconds(time + attackTextDuration);

        if (candyDifference > Inventory.GetInventoryWeapons().Count) candyDifference = prevCount;

        string formatted;

        if (candyDifference == 1)
        {
            formatted = "candy";
        }
        else
        {
            formatted = "candies";
        }

        txt = string.Format("Haha! I took " + candyDifference + " {0} from you!", formatted);
        time = 0f;

        foreach (char c in txt.ToCharArray()) time += messageSpeed;

        StartCoroutine(Type(headerContainer, txt, messageSpeed));

        yield return new WaitForSeconds(time + attackTextDuration);

        if (Inventory.GetInventoryWeapons().Count == 0)
        {
            txt = "You have no more candy left! You got what you deserved *evil laughter*";
            time = 0f;

            foreach (char c in txt.ToCharArray()) time += messageSpeed;

            StartCoroutine(Type(headerContainer, txt, messageSpeed));

            yield return new WaitForSeconds(time + attackTextDuration);

            PauseMenu.TriggerGameOver(GameManager.instance.endingMessages[new System.Random().Next(GameManager.instance.endingMessages.Count)]);

            yield break;
        }

        Inventory.UpdateWindow();
        Inventory.UpdateView();
        Inventory.Show();

        canAttack = true;
        headerContainer.text = "What will you use against your Mom?";
    }

    private IEnumerator DrainHealth(int drainAmount)
    {
        var temp = bossHealth - drainAmount;

        if (Inventory.GetInventoryWeapons().Count == 0 && temp != 0)
        {
            if (attackResetState != null) StopCoroutine(attackResetState);

            string txt = "You have no more candy left! You got what you deserved *evil laughter*";
            float time = 0f;

            foreach (char c in txt.ToCharArray()) time += 0.02f;

            StartCoroutine(BossEndMessage(headerContainer, txt, 0.02f));

            yield break;
        }

        for (int i = 0; i < BossHealth - (BossHealth - drainAmount); i++)
        {
            BossHealth -= 1;
            bossHP.text = "HP ♥ " + bossHealth.ToString();

            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator DrainCandy(int drainAmount)
    {
        for (int i = 0; i < drainAmount; i++)
        {
            if (Inventory.GetInventoryWeapons().Count != 0) Inventory.RemoveWeapon(Inventory.GetInventoryWeapons()[new System.Random().Next(Inventory.GetInventoryWeapons().Count)]);
            candyAmount.text = "x " + Inventory.GetInventoryWeapons().Count.ToString();

            yield return new WaitForSeconds(0.15f);
        }
    }

    private void CheckInventory()
    {
        if (Inventory.GetInventoryWeapons().Count == 0)
        {
            PauseMenu.TriggerGameOver("You got no candy from trick or treating! Why...");
        }
        else
        {
            AudioManager.instance.Play("Boss Theme", true);
        }
    }
}