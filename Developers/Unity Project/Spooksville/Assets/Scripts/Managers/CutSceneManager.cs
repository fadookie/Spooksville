﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class CutSceneManager : MonoBehaviour
{
    private List<string> dialogCSOne;
    private List<string> dialogCSTwo;

    [Header("Colors")]
    public Color playerTextColor;
    public Color momTextColor;

    private float time = 3f;

    private float duration;
    private int dialogIndex;

    private void Start()
    {
        dialogCSOne = new List<string>();
        dialogCSTwo = new List<string>();
        ReadDialog();

        if (SceneManager.GetActiveScene().name == "CutScene 1")
        {
            StartCoroutine(BeginSequence(dialogCSOne, 1));
        }

        if (SceneManager.GetActiveScene().name == "CutScene 2")
        {
            StartCoroutine(BeginSequence(dialogCSTwo, 2));
        }
    }

    private void ReadDialog()
    {
        string dialog = "Assets/Utility/Dialog/CutScene 1 Dialog.txt";
        string dialogTwo = "Assets/Utility/Dialog/CutScene 2 Dialog.txt";

        StreamReader reader = new StreamReader(dialog);

        while (!reader.EndOfStream)
        {
            dialogCSOne.Add(reader.ReadLine());
        }

        reader = new StreamReader(dialogTwo);


        while (!reader.EndOfStream)
        {
            dialogCSTwo.Add(reader.ReadLine());
        }

        reader.Close();
    }

    private IEnumerator BeginSequence(List<string> dialog, int buildScene)
    {
        float typeTime = 0.05f;

        Text container = GameObject.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>();

        foreach (string txt in dialog)
        {
            duration = 0f;

            foreach (char letter in txt.ToCharArray())
            {
                float wait = typeTime;

                if (letter == '!' || letter == '.' || letter == '?') wait = 1f;
                if (letter == ',') wait = 0.65f;

                duration += wait;
            }

            StartCoroutine(Type(container, dialog[dialogIndex], typeTime));

            yield return new WaitForSeconds(duration + time);
            dialogIndex++;
        }

        SceneManager.LoadScene(buildScene);
    }

    private IEnumerator Type(Text textContainer, string text, float typeTime)
    {
        textContainer.text = "";

        if (dialogIndex % 2 == 0)
        {
            textContainer.color = playerTextColor;
        } else
        {
            textContainer.color = momTextColor;
        }

        foreach (char letter in text.ToCharArray())
        {
            var wait = typeTime;

            textContainer.text += letter;

            if (letter == '!' || letter == '.' || letter == '?') wait = 1f;
            if (letter == ',') wait = 0.65f;

            yield return new WaitForSeconds(wait);
        }
    }
}
