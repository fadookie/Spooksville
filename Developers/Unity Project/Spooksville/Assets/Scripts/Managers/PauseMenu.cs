using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    public Canvas menu;
    private static GameObject gameOver;

    private void Start()
    {
        if (instance == null) instance = this;

        menu.gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameOver = GameObject.Find("Game Over Screen");
            gameOver.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Pause") == 1) DisplayMenu();
    }

    private void DisplayMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (GameObject.Find("Game Over Screen").gameObject.activeInHierarchy) return;
        }

        GameManager.instance.PauseGame();

        menu.gameObject.SetActive(true);
    }

    public static void TriggerGameOver(string message)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            gameOver.SetActive(true);
            gameOver.transform.Find("Message").GetComponent<Text>().text = message;
            AudioManager.instance.StopAll();
            BattleManager.instance.StopAllCoroutines();
            AudioManager.instance.Play("Game Over");
        }
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

    public void OnRestart()
    {
        FadeAnimation.instance.LoadScene(0);
        GameManager.instance.ResetGame();
    }
}
