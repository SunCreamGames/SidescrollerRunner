using System.Collections;
using System.Collections.Generic;
using Signals;
using UnityEngine;
using Views;
using Zenject;

public class Flow : View
{
    private SignalBus _signalBus;

    [Inject]
    public void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;

        signalBus.Subscribe<CollisionWithObstacle>(OnPlayerDied);
        _signalBus.Subscribe<LevelRestarting>(StartLevel);
    }

    private void StartLevel()
    {
        _signalBus.Fire<LevelStarting>();
    }

    private void OnPlayerDied()
    {
        _signalBus.Fire<LevelFailing>();
    }

    void Start()
    {
    }

    void Update()
    {
    }
}