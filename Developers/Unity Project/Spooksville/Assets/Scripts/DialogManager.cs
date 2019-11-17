using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Text container;
    public float typeSpeed = 0.02f;
    [Range(0.05f, .1f)]
    public float fadeSpeed = .1f;

    private List<string> collectionMessages = new List<string>();

    private Coroutine currentTypeRoutine;
    private Coroutine removeTextRoutine;

    private bool canFadeText;

    private void Start()
    {
        if (instance == null)
            instance = this;

        container.gameObject.SetActive(false);
        InitCollectionMessages();
    }

    private void Update()
    {

    }

    #region Code
    public void DisplayRandomCollectText()
    {
        string text = collectionMessages[(new System.Random()).Next(collectionMessages.Count)];

        RenderText(text, 2.5f);
    }

    public void RenderText(string text)
    {
        GetTextContainer().gameObject.SetActive(true);
        GetTextContainer().text = text;
    }

    public void RenderText(string text, float time)
    {
        GetTextContainer().gameObject.SetActive(true);

        if (removeTextRoutine != null) StopCoroutine(removeTextRoutine);
        if (currentTypeRoutine != null) StopCoroutine(currentTypeRoutine);
        currentTypeRoutine = StartCoroutine(Type(text, time));
    }

    private Text GetTextContainer()
    {
        return container;
    }
    #endregion 

    private void InitCollectionMessages()
    {
        string path = "Assets/Utility/Dialog/Town.txt";

        StreamReader reader = new StreamReader(path);

        string text = reader.ReadToEnd();
        string[] messages = text.Split('/');

        collectionMessages.AddRange(messages);

        reader.Close();
    }

    private IEnumerator RemoveText(float time)
    {
        yield return new WaitForSeconds(time);
        GetTextContainer().gameObject.SetActive(false);
    }

    private IEnumerator Type(string text)
    {
        GetTextContainer().text = "";

        foreach (char letter in text.ToCharArray())
        {
            container.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        if (removeTextRoutine != null) StopCoroutine(removeTextRoutine);
        removeTextRoutine = StartCoroutine(RemoveText(1f));
    }

    private IEnumerator Type(string text, float time)
    {
        GetTextContainer().text = "";

        foreach (char letter in text.ToCharArray())
        {
            container.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }

        removeTextRoutine = StartCoroutine(RemoveText(time));
    }

    private void FadeText()
    {
        if (canFadeText)
        {
            //DO THIS!
        }
    }
}
