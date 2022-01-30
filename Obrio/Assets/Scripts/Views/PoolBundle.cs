using System;

namespace Views
{
    using System.Collections.Generic;
    using UnityEngine;

    public class PoolBundle : View
    {
        private Dictionary<string, Pool> _pools;

        [SerializeField]
        private List<View> _poolingPrefabs;

        [SerializeField]
        private Pool _poolPrefab;

        private void Awake()
        {
            _pools = new Dictionary<string, Pool>();
            foreach (var prefab in _poolingPrefabs)
            {
                var pool = Instantiate(_poolPrefab);
                pool.Init(prefab, 50, new Vector3(0, 50f, 0f));
                _pools[prefab.name] = pool.GetComponent<Pool >();
            }
        }

        public View GetObject(string tag)
        {
            return _pools[tag].GetObject();
        }

        public void ReturnObject(string tag, View gameObject)
        {
            _pools[tag].Return(gameObject);
        }
    }
}