using Logic;
using Signals;
using UnityEngine.PlayerLoop;

namespace Views
{
    using System.Collections.Generic;
    using Logic.Generating;
    using UnityEngine;
    using Zenject;

    public class TileView : View
    {
        [SerializeField]
        private Pools _pools;

        private List<PlatformView> _obstacles;
        private List<CoinView> _coins;
        private GroundView _ground;
        private float _speed;
        private IGameSpeedController _gameSpeedController;
        private ILevelCreator _levelCreator;

        void Awake()
        {
            _speed = 0f;
        }

        [Inject]
        public void Init(SignalBus signalBus, ILevelCreator levelCreator, IGameSpeedController gameSpeedController)
        {
            _levelCreator = levelCreator;
            _obstacles = new List<PlatformView>();
            _coins = new List<CoinView>();
            _gameSpeedController = gameSpeedController;
            signalBus.Subscribe<SpeedUpdated>(s => UpdateSpeed(s.NewSpeed));
            signalBus.Subscribe<LevelFailing>(() => UpdateSpeed(0));
        }

        public void UpdateSpeed(float newSpeed)
        {
            _speed = newSpeed;
        }

        public void StartMove()
        {
            _speed = _gameSpeedController.Speed;
        }

        void Update()
        {
            transform.position = transform.position - new Vector3(-_speed, 0, 0) * Time.deltaTime;
            TryToDecompose();
        }

        private void TryToDecompose()
        {
        }
    }
}