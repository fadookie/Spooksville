using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    public static DialogManager instance;

    public Text container;
    private List<string> collectionMessages = new List<string>();

    private void Start()
    {
        if (instance == null)
            instance = this;

        container.gameObject.SetActive(false);
        collectionMessages.Add("Get out of here!");
    }

    #region Code
    public void DisplayRandomCollectText()
    {
        string text = collectionMessages[Mathf.RoundToInt(Random.Range(0, collectionMessages.Count - 1))];

        RenderText(text, 3f);
    }

    public void RenderText(string text)
    {
        GetTextContainer().gameObject.SetActive(true);
        GetTextContainer().text = text;
    }

    public void RenderText(string text, float time)
    {
        GetTextContainer().gameObject.SetActive(true);
        GetTextContainer().text = text;

        Invoke("DeactivateTextContainer", time);
    }

    private void DeactivateTextContainer()
    {
        GetTextContainer().gameObject.SetActive(false);
    }

    private Text GetTextContainer()
    {
        return container;
    }

    #endregion 
}
