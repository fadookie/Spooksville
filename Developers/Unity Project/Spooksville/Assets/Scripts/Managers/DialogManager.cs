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
        TextAsset txt = (TextAsset)Resources.Load("Town");
        string fixedText = txt.text.Replace(System.Environment.NewLine, "");
        foreach (string log in fixedText.Split('/')) collectionMessages.Add(log);
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

            yield return new WaitForSeconds(typeSpeed);
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
