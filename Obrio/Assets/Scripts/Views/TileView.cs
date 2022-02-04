using System.Diagnostics;
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

    public class TileView : MonoBehaviour
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

        [SerializeField]
        private PoolBundle _poolBundle;

        private List<Platform> _obstacles;
        private List<Coin> _coins;
        private Ground _ground;

        private float _speed;


        void Start()
        {
            _speed = 0f;
            _isLastTileOnMap = false;

            _obstacles = new List<Platform>();
            _coins = new List<Coin>();

            SignalBus.Subscribe<SpeedUpdated>(s => UpdateSpeed(s.NewSpeed));
            SignalBus.Subscribe<PickUpCoin>(OnPickedUpCoin);
        }

        private void OnPickedUpCoin(PickUpCoin signal)
        {
            if (!_coins.Contains(signal.Coin))
            {
                return;
            }

            _coins.Remove(signal.Coin);
            signal.Coin.transform.SetParent(null);
            _poolBundle.ReturnObject("Coin", signal.Coin.gameObject);
        }


        public void UpdateSpeed(float newSpeed)
        {
            rb.velocity = new Vector2(-newSpeed, 0f);
        }

        public void StartMove()
        {
            rb.velocity = new Vector2(-GameSpeedController.Speed, 0f);
        }

        void Update()
        {
            TrySpawnNewTile();
            TryDecompose();
        }

        private void TrySpawnNewTile()
        {
            if (_isLastTileOnMap && transform.position.x < 100f && transform.position.y < 25f)
            {
                _isLastTileOnMap = false;
                SignalBus.Fire(new SpawnTile {LastTile = this});
            }
        }

        private void TryDecompose()
        {
            if (transform.position.x < -50f)
            {
                Decompose();
            }
        }

        public void Compose(Tile tile, PoolBundle poolBundle)
        {
            SignalBus.Subscribe<StartMoving>(StartMove);
            SignalBus.Subscribe<LevelRestarting>(Decompose);

            _poolBundle = poolBundle;
            _isLastTileOnMap = true;
            _coins = new List<Coin>();
            _obstacles = new List<Platform>();

            for (int i = 0; i < tile.Coins.Count; i++)
            {
                if (tile.Coins[i])
                {
                    var coin = _poolBundle.GetObject("Coin");
                    coin.transform.SetParent(_coinsSlots[i]);
                    coin.transform.localPosition = Vector3.zero;
                    _coins.Add(coin.GetComponent<Coin>());
                }
            }

            for (int i = 0; i < tile.Obstacles.Count; i++)
            {
                if (tile.Obstacles[i])
                {
                    var platform = _poolBundle.GetObject("Platform");
                    platform.transform.SetParent(_obtacleSlots[i]);
                    platform.transform.localPosition = Vector3.zero;

                    _obstacles.Add(platform.GetComponent<Platform>());
                }
            }

            TrySpawnNewTile();
            StartMove();
        }

        private void Decompose()
        {
            rb.velocity = Vector2.zero;
            foreach (var coin in _coins)
            {
                coin.transform.SetParent(null);
                _poolBundle.ReturnObject("Coin", coin.gameObject);
            }

            foreach (var platform in _obstacles)
            {
                platform.transform.SetParent(null);
                _poolBundle.ReturnObject("Platform", platform.gameObject);
            }

            _poolBundle.ReturnObject("Tile", gameObject);
            SignalBus.Unsubscribe<StartMoving>(StartMove);
            SignalBus.Unsubscribe<LevelRestarting>(Decompose);
        }

        public void SetIsLastTile(bool b)
        {
            _isLastTileOnMap = b;
        }
    }
}