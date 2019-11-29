using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeAnimation : MonoBehaviour
{
    public static FadeAnimation instance;
    private Animator anim;
    
    private int scene;

    private void Start()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
        anim.SetTrigger("FadeIn");
    }

    public void LoadScene(int sceneIndex)
    {
        scene = sceneIndex;
        Debug.Log("Scene was loaded");
        anim.SetTrigger("FadeOut");
    }

    public void FadeInAnimation()
    {
        //IMplement raw version
        anim.SetTrigger("FadeInRaw");
    }

    public void FadeOutAnimation()
    {
        anim.SetTrigger("FadeOutRaw");
    }

    private void LoadIndicatedScene()
    {
        SceneManager.LoadScene(scene);
    }
}
