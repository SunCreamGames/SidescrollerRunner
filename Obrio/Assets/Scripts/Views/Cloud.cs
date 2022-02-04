using System;
using Logic;
using Signals;
using UnityEngine;
using Zenject;

namespace Views
{
    public class Cloud : MonoBehaviour
    {
        [Inject]
        private SignalBus _signalBus;

        [Inject]
        private IGameSpeedController _gameSpeedController;

        [SerializeField]
        private Rigidbody2D rb;

        private void Start()
        {
            _signalBus.Subscribe<SpeedUpdated>(s => UpdateSpeed(s.NewSpeed * 0.7f));
        }

        private void UpdateSpeed(float newSpeed)
        {
            rb.velocity = new Vector2(-newSpeed, 0f);
        }

        public void StartMove()
        {
            rb.velocity = new Vector2(-0.7f * _gameSpeedController.Speed, 0f);
        }
    }
}