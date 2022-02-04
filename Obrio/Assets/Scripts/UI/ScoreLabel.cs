using System.Collections;
using System.Collections.Generic;
using Logic;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ScoreLabel : MonoBehaviour
{
    [Inject]
    private IScoreCounter _scoreCounter;

    [Inject]
    private SignalBus _signalBus;

    [SerializeField]
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _signalBus.Subscribe<UpdateScore>(signal => UpdateLabel(signal.Score));
    }

    private void UpdateLabel(int score)
    {
        _text.text = score.ToString();
    }
}