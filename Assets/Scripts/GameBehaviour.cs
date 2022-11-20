using UnityEngine;
using StateMachine;
using System;

public class GameBehaviour : GamePlayBehaviour
{
    public static GameBehaviour instance;

    public delegate void _onTakingCoins();
    public _onTakingCoins OnTakingCoins;

    PlayerBehaviour _player;

    [SerializeField] GameObject _pauseMenu, _gameOverMenu;

    [SerializeField] int _playerMoney;
    public int PlayerMoney { get { return _playerMoney; } set { _playerMoney = value; } }

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
    }

    private void Start()
    {
        instance = this;

        NavigationData.OnLoading?.Invoke();

        OnNextGameState(GamePlayStates.INITIALIZING);
    }

    private void Update()
    {
        StateBehaviour(GamePlayCurrentState);

        UpdateState();
    }

    void StateBehaviour(GamePlayStates state)
    {
        switch(state)
        {
            case GamePlayStates.INITIALIZING:
                {
                    _gameOverMenu.SetActive(false);
                    _pauseMenu.SetActive(false);

                    CameraBehaviour.OnSearchingPlayer?.Invoke();
                    
                    break;
                }
            case GamePlayStates.START:
                {
                    OnNextGameState.Invoke(GamePlayStates.GAMEPLAY);

                    break;
                }
            case GamePlayStates.GAMEPLAY:
                {
                    Time.timeScale = 1;

                    break;
                }
            case GamePlayStates.PAUSE:
                {
                    Time.timeScale = 0;

                    break;
                }
            case GamePlayStates.GAMEOVER:
                {
                    _gameOverMenu.SetActive(true);

                    Time.timeScale = 0;

                    break;
                }
        }
    }

    //METHOD TO BE CALLED ON PLAYER ACTION 'ON PAUSING'
    //METHOD TO BE CALLED ALSO ON BUTTON EVENT
    public void PauseGame()
    {
        if (GetCurrentGameState() != GamePlayStates.PAUSE)
        {
            _pauseMenu.SetActive(true);

            OnNextGameState?.Invoke(GamePlayStates.PAUSE);
        }
        else
        {
            _pauseMenu.SetActive(false);

            OnNextGameState?.Invoke(GamePlayStates.GAMEPLAY);
        }
    }

    public void UpdateCoins()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Currency Show");
        temp.GetComponent<TMPro.TMP_Text>().text = PlayerMoney.ToString();
    }

    private void OnEnable()
    {
        OnNextGameState += NextGameStates;
        _player.OnPausing += PauseGame;
        OnTakingCoins += UpdateCoins;
    }
    private void OnDisable()
    {
        OnNextGameState -= NextGameStates;
        _player.OnPausing -= PauseGame;
    }
}
