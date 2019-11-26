using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] public bool IsPaused { get; private set; }

    [SerializeField] private float timeUntillBossBattle = 60;
    private float time;

    private bool hasLoaded;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

    }

    #endregion

    private void TownTimer()
    {
        if (!IsPaused)
        {
            time += Time.deltaTime;

            if (SceneManager.GetActiveScene().name == "Town")
            {
                if (!hasLoaded)
                {
                    if (time > timeUntillBossBattle)
                    {
                        SceneManager.LoadScene(4);
                        hasLoaded = true;
                    }
                }
            }
        }
    }

    #region Pause Menu

    public void PauseGame()
    {
        if (IsPaused) return;

        IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;

        IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    #endregion
}