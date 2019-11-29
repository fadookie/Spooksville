using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CutSceneManager : MonoBehaviour
{
    public Canvas canvas;

    private List<string> dialogCSOne;
    private List<string> dialogCSTwo;
    private List<string> dialogCSThree;

    [Header("Colors")]
    public Color staticTextColor;
    public Color playerTextColor;
    public Color momTextColor;

    private float time = 3f;

    private float duration;
    private int dialogIndex;

    private int buildIndex;
    private bool isLoading;

    private enum ColorMode
    {
        Alternating, Static
    }

    private void Start()
    {
        AudioManager.instance.StopAll();

        dialogCSOne = new List<string>();
        dialogCSTwo = new List<string>();
        dialogCSThree = new List<string>();

        ReadDialog();

        if (SceneManager.GetActiveScene().name == "CutScene 1")
        {
            StartCoroutine(BeginSequence(dialogCSOne, 1));
        }

        if (SceneManager.GetActiveScene().name == "CutScene 2")
        {
            StartCoroutine(BeginSequence(dialogCSTwo, 2));
        }

        if (SceneManager.GetActiveScene().name == "CutScene 3")
        {
            StartCoroutine(BeginSequence(dialogCSThree, 0));
            GameManager.instance.ResetGame();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !isLoading)
        {
            isLoading = true;
            FadeAnimation.instance.LoadScene(buildIndex);
        }
    }

    private void ReadDialog()
    {
        TextAsset txt = (TextAsset)Resources.Load("CutScene 1 Dialog");
        string fixedText = txt.text.Replace(System.Environment.NewLine, "");
        foreach (string log in fixedText.Split('/')) dialogCSOne.Add(log);

        txt = (TextAsset)Resources.Load("CutScene 2 Dialog");
        fixedText = txt.text.Replace(System.Environment.NewLine, "");
        foreach (string log in fixedText.Split('/')) dialogCSTwo.Add(log);

        txt = (TextAsset)Resources.Load("CutScene 3 Dialog");
        fixedText = txt.text.Replace(System.Environment.NewLine, "");
        foreach (string log in fixedText.Split('/')) dialogCSThree.Add(log);
    }

    private IEnumerator BeginSequence(List<string> dialog, int buildScene)
    {
        buildIndex = buildScene;

        float typeTime = 0.05f;
        int index = 0;

        Text container = canvas.gameObject.transform.Find("Text").gameObject.GetComponent<Text>();

        foreach (string txt in dialog)
        {
            duration = 0f;

            foreach (char letter in txt.ToCharArray())
            {
                float wait = typeTime;

                if (index != txt.Length - 1)
                {
                    if (letter == '!' || letter == '.' || letter == '?') wait = 1f;
                    if (letter == ',') wait = 0.65f;
                }

                duration += wait;
                index++;
            }

            if (buildScene == 5)
            {
                StartCoroutine(Type(container, dialog[dialogIndex], typeTime, ColorMode.Static));
            } else
            {
                StartCoroutine(Type(container, dialog[dialogIndex], typeTime, ColorMode.Alternating));
            }

            yield return new WaitForSeconds(duration + time);
            dialogIndex++;
        }

        FadeAnimation.instance.LoadScene(buildScene);
    }

    private IEnumerator Type(Text textContainer, string text, float typeTime, ColorMode mode)
    {
        textContainer.text = "";

        if (mode == ColorMode.Alternating)
        {
            if (dialogIndex % 2 == 0)
            {
                textContainer.color = playerTextColor;
            }
            else
            {
                textContainer.color = momTextColor;
            }
        }

        if (mode == ColorMode.Static)
        {
            textContainer.color = staticTextColor;
        }

        foreach (char letter in text.ToCharArray())
        {
            var wait = typeTime;

            textContainer.text += letter;

            if (letter == '!' || letter == '.' || letter == '?') wait = 1f;
            if (letter == ',') wait = 0.65f;

            AudioManager.instance.PlayIfNotPlaying("Talking");

            yield return new WaitForSeconds(wait);
        }
    }
}
