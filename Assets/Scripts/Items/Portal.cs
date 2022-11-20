using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [Tooltip("NEXT SCENE NAME")]
    [SerializeField] string _nextScene;
    public string NextScene { get { return _nextScene; } set { _nextScene = value; } }
    [Space(10)]
    [SerializeField] GameObject txtAction;

    public void MovingNextScene()
    {
        StartCoroutine(LoadingGamePlay(NextScene));
    }

    public void ShowInfoUI(bool show, string txt)
    {
        txtAction.SetActive(show);
        txtAction.GetComponentInChildren<TMPro.TMP_Text>().text = txt;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehaviour p = collision.gameObject.GetComponent<PlayerBehaviour>();

        if (p != null)
        {
            collision.GetComponent<PlayerBehaviour>().OnActing += MovingNextScene;
            collision.GetComponent<PlayerBehaviour>().OnActing -= collision.GetComponent<PlayerBehaviour>().Attacking;
            ShowInfoUI(true, "ENTER");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerBehaviour p = collision.gameObject.GetComponent<PlayerBehaviour>();

        //SE A COLISAO DETECTAR QUE O GAMEOBJECT POSSUI A CLASSE 'PLAYER'
        if (p != null)
        {
            collision.GetComponent<PlayerBehaviour>().OnActing -= MovingNextScene;
            ShowInfoUI(false, "ENTER");
        }

    }

    public IEnumerator LoadingGamePlay(string scene)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(scene);

        while (!loading.isDone)
        {
            yield return null;
        }
    }
}
