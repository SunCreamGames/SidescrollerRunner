using Logic;

namespace Input
{
    using System;
    using Signals;
    using UnityEngine;
    using Views;
    using Zenject;

    public class PlayerMovement : MonoBehaviour
    {
        [Inject]
        public SignalBus SignalBus { get; set; }

        [Inject]
        private Player _player;

        private bool _isPlaying;

        private void Awake()
        {
            _isPlaying = false;
            SignalBus.Subscribe<LevelStarted>(() => _isPlaying = true);
            SignalBus.Subscribe<LevelFailing>(() => { _isPlaying = false; });
        }

        private void Update()
        {
            if (_isPlaying && Input.GetKeyDown(KeyCode.Space))
            {
                SignalBus.Fire<PlayerTryJump>();
            }
        }
    }
}