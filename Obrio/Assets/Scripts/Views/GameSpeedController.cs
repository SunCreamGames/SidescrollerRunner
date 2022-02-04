namespace Views
{
    using Logic;
    using Signals;
    using UnityEngine;
    using Zenject;

    public class GameSpeedController : MonoBehaviour
    {
        private SignalBus _signalBus;
        private IGameSpeedController _gameSpeedController;

        private float _speed;
        private float _dist;

        [Inject]
        public void Init(SignalBus signalBus, IGameSpeedController gameSpeedController)
        {
            _signalBus = signalBus;
            _gameSpeedController = gameSpeedController;
            _speed = _gameSpeedController.Speed;
            _dist = 0f;
        }


        void Update()
        {
            _dist += _speed * Time.deltaTime;
            TryUpdateSpeed();
        }

        private void TryUpdateSpeed()
        {
            if (_dist > _gameSpeedController.SpeedPoint)
            {
                _signalBus.Fire<UpdateSpeed>();
            }
        }
    }
}