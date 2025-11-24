using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

[RequireComponent(typeof(CinemachineBrain))]
[RequireComponent(typeof(CinemachineCamera))]
public class CameraBehaviour : MonoBehaviour
{
    public delegate void _onSearchingPlayer();
    public static _onSearchingPlayer OnSearchingPlayer;

    GameObject p;

    void FindPlayer()
    {
        StartCoroutine(CorroutineFindPlayer());
    }

    IEnumerator CorroutineFindPlayer()
    {
        p = GameObject.FindGameObjectWithTag("Player");

        yield return new WaitForSeconds(.02f);

        if (p != null)
        {
            GetComponent<CinemachineCamera>().Follow = p.transform;

            GameBehaviour.OnNextGameState?.Invoke(GamePlayStates.START);

            StopCoroutine(CorroutineFindPlayer());
        }
        else
        {
            yield return new WaitForSeconds(.02f);
        }
    }

    private void OnEnable()
    {
        OnSearchingPlayer += FindPlayer;
    }
    private void OnDisable()
    {
        OnSearchingPlayer -= FindPlayer;
    }
}
