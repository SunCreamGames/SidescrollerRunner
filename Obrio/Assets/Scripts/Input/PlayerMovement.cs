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
        private bool _shouldRestart;

        private void Awake()
        {
            _isPlaying = _shouldRestart = false;
            SignalBus.Subscribe<LevelStarting>(() => _isPlaying = true);
            SignalBus.Subscribe<LevelFailing>(() =>
            {
                _isPlaying = false;
                _shouldRestart = true;
            });
        }

        private void Update()
        {
            if (!_isPlaying)
            {
                if (Input.anyKeyDown)
                {
                    if (_shouldRestart)
                    {
                        SignalBus.Fire<LevelRestarting>();
                    }
                    else
                    {
                        SignalBus.Fire<LevelStarting>();
                    }

                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SignalBus.Fire<PlayerTryJump>();
            }
        }
    }
}