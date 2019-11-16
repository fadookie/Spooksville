using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timeUntillBossBattle = 60;
    private float startTime;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        startTime = Time.time;
    }

    private void Update()
    {
        TownTimer();
    }

    private void TownTimer()
    {
        if (Time.time - startTime > timeUntillBossBattle)
        {
            SceneManager.LoadScene("Boss Battle");
        }
    }
}
