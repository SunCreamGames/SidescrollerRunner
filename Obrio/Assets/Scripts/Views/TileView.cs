using System.Diagnostics;
using Logic;
using Signals;
using Unity.Profiling;
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
        List<GameObject> _decorations;

        private float _speed;


        void Start()
        {
            _speed = 0f;
            _isLastTileOnMap = false;
            _decorations = new List<GameObject>();
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

            var a = new System.Random();
            var decorationsCount = a.Next(5);
            for (int i = 0; i < decorationsCount; i++)
            {
                if (i % 2 == 0)
                {
                    var bushe = _poolBundle.GetObject($"Bushe_{i / 2}");
                    bushe.transform.parent = transform;
                    bushe.transform.localPosition = new Vector3(Random.Range(-15f, 15f), -3.35f, 0f);

                    _decorations.Add(bushe);
                }

                else
                {
                    var tree = _poolBundle.GetObject($"Tree_{i / 2}");
                    tree.transform.parent = transform;
                    tree.transform.localPosition = new Vector3(Random.Range(-15f, 15f), 0f, 0f);
                    _decorations.Add(tree);
                }
            }

            TrySpawnNewTile();
            StartMove();
        }

        private void Decompose()
        {
            rb.velocity = Vector2.zero;
            while (_coins.Count > 0)
            {
                var coin = _coins[0];

                coin.transform.SetParent(null);
                _poolBundle.ReturnObject("Coin", coin.gameObject);
                _coins.Remove(coin);
            }

            while (_obstacles.Count > 0)
            {
                var platform = _obstacles[0];
                platform.transform.SetParent(null);
                _poolBundle.ReturnObject("Platform", platform.gameObject);
                _obstacles.Remove(platform);
            }

            while (_decorations.Count > 0)
            {
                var decoration = _decorations[0];
                _poolBundle.ReturnObject(decoration.name, decoration);
                decoration.transform.SetParent(null);
                _decorations.Remove(decoration);
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