using Logic;
using Signals;
using UnityEngine.PlayerLoop;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Views
{
    using System.Collections.Generic;
    using Logic.Generating;
    using UnityEngine;
    using Zenject;

    public class TileView : View
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private List<Transform> _obtacleSlots;

        [SerializeField]
        private List<Transform> _coinsSlots;

        [Inject]
        public SignalBus SignalBus;

        [Inject]
        public IGameSpeedController GameSpeedController;

        [Inject]
        public ILevelCreator LevelCreator;

        [SerializeField]
        private bool _isLastTileOnMap;

        private PoolBundle _poolBundle;
        private List<PlatformView> _obstacles;
        private List<CoinView> _coins;
        private GroundView _ground;

        private float _speed;

        //
        // [Inject]
        // public void Init(SignalBus signalBus, ILevelCreator levelCreator, IGameSpeedController gameSpeedController)
        // {
        //     SignalBus = signalBus;
        //     LevelCreator = levelCreator;
        //     GameSpeedController = gameSpeedController;
        //
        //     Start2();
        // }

        void Start()
        {
            _speed = 0f;
            _isLastTileOnMap = false;

            _obstacles = new List<PlatformView>();
            _coins = new List<CoinView>();

            SignalBus.Subscribe<SpeedUpdated>(s => UpdateSpeed(s.NewSpeed));
            SignalBus.Subscribe<LevelFailing>(() => UpdateSpeed(0));
        }


        public void UpdateSpeed(float newSpeed)
        {
            rb.velocity = new Vector2(-newSpeed, 0f);
        }

        public void StartMove()
        {
            rb.velocity = new Vector2(-GameSpeedController.Speed, 0f);
            Debug.Log($"<color=red> {rb.velocity} </color>");
        }

        void Update()
        {
            TrySpawnNewTile();
            TryDecompose();
        }

        private void TrySpawnNewTile()
        {
            if (_isLastTileOnMap && transform.position.x < 200f && transform.position.y < 25f)
            {
                _isLastTileOnMap = false;
                SignalBus.Fire(new SpawnTile {LastTile = this});
            }
        }

        private void TryDecompose()
        {
            if (transform.position.x < -150f)
            {
                Decompose();
            }
        }

        public void Compose(Tile tile, PoolBundle poolBundle)
        {
            _poolBundle = poolBundle;
            _isLastTileOnMap = true;
            _coins = new List<CoinView>();
            _obstacles = new List<PlatformView>();

            for (int i = 0; i < tile.Coins.Count; i++)
            {
                if (tile.Coins[i])
                {
                    var coin = _poolBundle.GetObject("Coin");
                    coin.transform.SetParent(transform);
                    coin.transform.position = _coinsSlots[i].position;
                    _coins.Add((CoinView) coin);
                }
            }

            for (int i = 0; i < tile.Obstacles.Count; i++)
            {
                if (tile.Obstacles[i])
                {
                    var platform = _poolBundle.GetObject("Platform");
                    var t = platform.transform;
                    t.SetParent(transform);
                    t.position = _obtacleSlots[i].position;
                    _obstacles.Add((PlatformView) platform);
                }
            }

            TrySpawnNewTile();
            SignalBus.Subscribe<LevelStarting>(StartMove);
            // StartMove();
        }

        private void Decompose()
        {
            rb.velocity = Vector2.zero;
            foreach (var coin in _coins)
            {
                coin.transform.SetParent(null);
                _poolBundle.ReturnObject("Coin", coin);
            }

            foreach (var platform in _obstacles)
            {
                platform.transform.SetParent(null);
                _poolBundle.ReturnObject("Platform", platform);
            }

            _poolBundle.ReturnObject("Tile", this);
        }

        public void SetIsLastTile(bool b)
        {
            _isLastTileOnMap = b;
        }
    }
}