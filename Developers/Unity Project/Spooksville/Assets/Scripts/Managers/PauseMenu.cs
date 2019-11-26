using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public Canvas menu;

    private void Start()
    {
        if (instance == null) instance = this;

        menu.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Pause") == 1) DisplayMenu();
    }

    private void DisplayMenu()
    {
        GameManager.instance.PauseGame();

        menu.gameObject.SetActive(true);
    }

    public void OnResume()
    {
        GameManager.instance.ResumeGame();

        menu.gameObject.SetActive(false);
    }

    public void OnQuitGame()
    {
        Application.Quit();
    }
}
