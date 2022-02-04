using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;

namespace Views
{
    public class PoolBundle : MonoBehaviour
    {
        private Dictionary<string, Pool> _pools;
        private DiContainer _container;


        [SerializeField]
        private List<GameObject> _poolingPrefabs;

        [SerializeField]
        private List<int> _sizes;

        [SerializeField]
        private Pool _poolPrefab;

        [Inject]
        public void Init(DiContainer container)
        {
            _container = container;
        }


        private void Awake()
        {
            _pools = new Dictionary<string, Pool>();
            for (var i = 0; i < _poolingPrefabs.Count; i++)
            {
                var prefab = _poolingPrefabs[i];
                var size = _sizes[i];


                var pool = Instantiate(_poolPrefab);
                // _poolPrefab.OnSpawnView += v => _container.InjectGameObject(v.gameObject);
                pool.Init(prefab, size, new Vector3(0, 50f, 0f), _container);
                _pools[prefab.name] = pool.GetComponent<Pool>();
            }
        }

        public GameObject GetObject(string tag)
        {
            return _pools[tag].GetObject();
        }

        public void ReturnObject(string tag, GameObject gameObject)
        {
            _pools[tag].Return(gameObject);
        }
    }
}