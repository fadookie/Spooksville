using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public List<string> endingMessages;

    [HideInInspector] public bool IsPaused { get; private set; }

    [SerializeField] private float timeUntillBossBattle = 60;
    private float time;
    private bool hasLoaded;

    private int timesPlayed;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        endingMessages.Add("The mom is not nice and she eat my candy, not very nice of her :(");
        endingMessages.Add("You tried your best, but clearly you didn’t try hard enough.");
        endingMessages.Add("Better luck next time!");
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1) TownTimer();
    }

    #region Scene Management

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1) AudioManager.instance.Play("Town Theme", true);
    }

    #endregion

    private void TownTimer()
    {
        if (!IsPaused)
        {
            time += Time.deltaTime;

            if (SceneManager.GetActiveScene().name == "Town")
            {
                if (time > timeUntillBossBattle && !hasLoaded)
                {
                    FadeAnimation.instance.LoadScene(4);
                    hasLoaded = true;
                }
            }
        }
    }

    #region Pause Menu

    public void PauseGame()
    {
        if (IsPaused) return;

        AudioManager.instance.PauseAll();

        IsPaused = true;
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;

        AudioManager.instance.UnPauseAll();

        IsPaused = false;
    }

    #endregion

    public void ResetGame()
    {
        Cursor.lockState = CursorLockMode.None;

        //Reset town
        hasLoaded = false;
        time = 0;

        //Update stats
        timesPlayed++;
    }
}