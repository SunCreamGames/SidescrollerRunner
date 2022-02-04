using Signals;
using UnityEngine;
using Views;
using Zenject;

public class Flow : View
{
    private SignalBus _signalBus;

    GameState _gameState;

    [Inject]
    public void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
        _gameState = GameState.GameNotStarted;
        signalBus.Subscribe<CollisionWithObstacle>(OnPlayerDied);
        _signalBus.Subscribe<LevelRestarting>(StartLevel);
    }

    private void StartLevel()
    {
        _signalBus.Fire<LevelStarting>();
        _gameState = GameState.GameIsPlaying;
    }

    private void OnPlayerDied()
    {
        Debug.Log($"<color=red> Level failed </color>");
        _signalBus.Fire<LevelFailing>();
        _gameState = GameState.GameIsOver;
    }

    void Update()
    {
        if (UnityEngine.Input.anyKeyDown)
        {
            if (_gameState == GameState.GameIsOver)
            {
                _signalBus.Fire<LevelRestarting>();
            }
            else if (_gameState == GameState.GameNotStarted)
            {
                _signalBus.Fire<LevelStarting>();
            }
        }
    }
}

enum GameState
{
    GameIsPlaying,
    GameIsOver,
    GameNotStarted
}