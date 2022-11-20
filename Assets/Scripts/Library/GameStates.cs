public enum GamePlayStates
{
    INITIALIZING,
    START,
    GAMEPLAY,
    PAUSE,
    GAMEOVER
}

namespace StateMachine
{
    public class GamePlayBehaviour : UnityEngine.MonoBehaviour
    {
        public delegate GamePlayStates _onNextGameState(GamePlayStates gameStates);
        public static _onNextGameState OnNextGameState;

        [UnityEngine.SerializeField] GamePlayStates _gamePlayPreviousState;
        [UnityEngine.SerializeField] GamePlayStates _gamePlayCurrentState;
        [UnityEngine.SerializeField] GamePlayStates _gamePlayNextState;

        public GamePlayStates GamePlayPreviousState
        { get { return _gamePlayPreviousState; } }

        public GamePlayStates GamePlayCurrentState
        { get { return _gamePlayCurrentState; } }

        public GamePlayStates GamePlayNextState
        { get { return _gamePlayNextState; } }

        public GamePlayStates NextGameStates(GamePlayStates newState)
        {
            _gamePlayPreviousState = _gamePlayCurrentState;
            return _gamePlayNextState = newState;
        }

        public GamePlayStates UpdateState()
        {
            return _gamePlayCurrentState = _gamePlayNextState ;
        }


        public GamePlayStates GetCurrentGameState()
        {
            return GamePlayCurrentState;
        }
    }
}
