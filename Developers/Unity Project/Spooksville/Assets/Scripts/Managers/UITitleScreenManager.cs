using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleScreenManager : MonoBehaviour
{
    private List<GameObject> screens;

    private void Start()
    {
        screens = new List<GameObject>();

        foreach (Canvas canvas in GameObject.Find("Screens").transform.GetComponentsInChildren<Canvas>())
        {
            screens.Add(canvas.gameObject);
            if (canvas.gameObject.name != "Title") canvas.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!AudioManager.instance.GetSound("Title Theme").source.isPlaying) AudioManager.instance.Play("Title Theme");
    }

    #region Title Screen

    public void OnPlayClick()
    {
        if (AudioManager.instance.GetSound("Title Theme").source.isPlaying) AudioManager.instance.Stop("Title Theme");
        SceneManager.LoadScene(1);
    }

    public void OnAboutClick()
    {
        LoadScreen("About");
    }

    #endregion Title Screen

    #region About Screen

    public void OnBackClick()
    {
        LoadScreen("Title");
    }

    #endregion

    public void LoadScreen(string screenName)
    {
        foreach (GameObject obj in screens) obj.SetActive(false);

        GameObject screen = GameObject.Find("Screens").transform.Find(screenName).gameObject;
        screen.SetActive(true);
    }
}