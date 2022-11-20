using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TimeCounter;

public class ChangeScene : Timer
{
    public delegate void _onChangeScene(string sceneName);
    public static _onChangeScene OnChangeScene;

    [SerializeField] bool _useTimer, _automaticSceneChange;

    [SerializeField] string _nextSceneName;

    private void Start()
    {
       if(_automaticSceneChange) ChangingScene(_useTimer, GetTimer);
    }

    private void Update()
    {
        CountDown();
    }
    void ChangingScene(bool useTime, float timer)
    {

        if (!useTime)
        {
            timer = 0;
        }

        SetTimer(timer, () => StartCoroutine(LoadingNextScene(_nextSceneName)));
    }

    #region - Button Methods

    public void SelectingNextScene(string sceneName)
    {
        StartCoroutine(LoadingNextScene(sceneName));
    }

    public void StartGame()
    {
        
        if(PlayerPrefs.HasKey(SaveLoadPlayerPrefs.SaveStrings.HAS_MADE_CHARACTER.ToString()))
        {
            StartCoroutine(LoadingNextScene("STORE"));
        }
        else
        {
            StartCoroutine(LoadingNextScene("CHARACTER_CREATION"));
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    public IEnumerator LoadingNextScene(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);

        while (!loading.isDone)
        {           
            yield return null;
        }
    }

    private void OnEnable()
    {
        OnChangeScene += SelectingNextScene;
    }
    private void OnDisable()
    {
        OnChangeScene -= SelectingNextScene;
    }
}
