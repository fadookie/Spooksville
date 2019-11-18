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
    private Color originalColor;

    public float typeSpeed = 0.02f;
    [Range(0f, 255f)]
    public float fadeSpeed = 10f;
    public float duration;

    private List<string> collectionMessages = new List<string>();

    private Coroutine currentTypeRoutine;
    private Coroutine removeTextRoutine;

    private bool canFadeText;

    private void Start()
    {
        if (instance == null) instance = this;

        container.gameObject.SetActive(false);
        originalColor = container.color;

        InitCollectionMessages();
    }

    private void Update()
    {
        FadeText();
    }

    #region Code
    public void DisplayRandomCollectText()
    {
        string text = collectionMessages[(new System.Random()).Next(collectionMessages.Count)];

        RenderText(text, duration);
    }

    public void RenderText(string text)
    {
        container.gameObject.SetActive(true);
        container.text = text;
    }

    public void RenderText(string text, float time)
    {
        container.gameObject.SetActive(true);
        canFadeText = false;
        container.color = originalColor;

        if (removeTextRoutine != null) StopCoroutine(removeTextRoutine);
        if (currentTypeRoutine != null) StopCoroutine(currentTypeRoutine);
        currentTypeRoutine = StartCoroutine(TypeCollectionDialogue(text, time));
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
        canFadeText = true;
    }

    public IEnumerator Type(Text textContainer, string text, float time)
    {
        textContainer.text = "";

        foreach (char letter in text.ToCharArray())
        {
            textContainer.text += letter;
            yield return new WaitForSeconds(time);
        }
    }

    private IEnumerator TypeCollectionDialogue(string text, float time)
    {
        container.text = "";

        foreach (char letter in text.ToCharArray())
        {
            container.text += letter;

            if (!AudioManager.instance.GetSound("Talking").source.isPlaying)
            {
                AudioManager.instance.Play("Talking");
            }

            yield return new WaitForSeconds(typeSpeed);
        }

        if (AudioManager.instance.GetSound("Talking").source.isPlaying)
        {
            AudioManager.instance.Stop("Talking", .3f);
        }

        removeTextRoutine = StartCoroutine(RemoveText(time));
    }

    private void FadeText()
    {
        if (canFadeText)
        {
            if (container.color.a - (fadeSpeed / 255f) <= 0)
            {
                container.color = container.color = new Color(container.color.r, container.color.g, container.color.b, 0f);
                canFadeText = false;
                return;
            }

            container.color = new Color(container.color.r, container.color.g, container.color.b, container.color.a - (fadeSpeed / 255f));
        }
    }
}
