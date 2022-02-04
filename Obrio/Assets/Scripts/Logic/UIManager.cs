using System;
using Signals;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _levelFailedWindow;

    [SerializeField]
    private GameObject _levelStartWindow;

    [Inject]
    private SignalBus _signalBus;

    private void Awake()
    {
        _levelStartWindow.SetActive(true);
        _signalBus.Subscribe<LevelFailing>(OnLevelFailed);
        _signalBus.Subscribe<LevelStarting>(OnLevelStarted);


        _levelStartWindow.SetActive(true);
        _levelFailedWindow.SetActive(false);
    }

    private void OnLevelStarted()
    {
        _levelFailedWindow.SetActive(false);
        _levelStartWindow.SetActive(false);
    }

    private void OnLevelFailed()
    {
        _levelFailedWindow.SetActive(true);
    }


    void Update()
    {
    }
}