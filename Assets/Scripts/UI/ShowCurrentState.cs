using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowCurrentState : MonoBehaviour
{
    TMP_Text txt;

    private void Awake()
    {
        txt = this.GetComponent<TMP_Text>();
    }

    void Start()
    {
        GameBehaviour.OnNextGameState += ShowText;    
    }

    GamePlayStates ShowText(GamePlayStates state)
    {
        txt.text = "Game State = " + state;

        return state;
    }
}
