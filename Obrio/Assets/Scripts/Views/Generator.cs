using Logic.Generating;
using Signals;
using UnityEngine;
using Zenject;

namespace Views
{
    public class Generator : View
    {
        [SerializeField]
        private PoolBundle _pools;

        private SignalBus _signalBus;
        private ILevelCreator _levelCreator;

        [Inject]
        public void Init(SignalBus signalBus, ILevelCreator levelCreator)
        {
            _levelCreator = levelCreator;
            _signalBus = signalBus;
            _signalBus.Subscribe<SpawnTile>(x => SpawnTile(x.LastTile));
        }

        void Start()
        {
            _signalBus.Subscribe<LevelRestarting>(InitLevel);
            InitLevel();
        }

        private void InitLevel()
        {
            var first = _levelCreator.CreateEmptyTile();
            var tileView = _pools.GetObject("Tile");
            tileView.transform.position = new Vector3(0, 0f, 0f);
            tileView.GetComponent<TileView>().Compose(first, _pools);
        }

        private void SpawnTile(TileView lastTile)
        {
            var tile = _levelCreator.CreateTile();
            var tileView = _pools.GetObject("Tile");
            tileView.GetComponent<TileView>().SetIsLastTile(true);
            tileView.transform.position = lastTile.transform.position + new Vector3(33f, 0f, 0f);
            tileView.GetComponent<TileView>().Compose(tile, _pools);
        }
    }
}