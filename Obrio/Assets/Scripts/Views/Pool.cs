using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Views
{
    public class Pool : MonoBehaviour

    {
        private int _size;
        private GameObject _prefab;
        private Vector3 _poolRoot;
        private Queue<GameObject> _objects;

        public GameObject GetObject()
        {
            var res = _objects.Dequeue();
            return res;
        }

        public void Return(GameObject o)
        {
            o.transform.position = _poolRoot;
            _objects.Enqueue(o);
        }

        public void Init(GameObject prefab, int size, Vector3 rootPos, DiContainer container)
        {
            _prefab = prefab;
            _poolRoot = rootPos;
            _size = size;

            _objects = new Queue<GameObject>();
            for (int i = 0; i < _size; i++)
            {
                var a = Instantiate(_prefab);
                container.InjectGameObject(a.gameObject);
                a.transform.position = _poolRoot;
                a.gameObject.SetActive(true);
                _objects.Enqueue(a);
            }
        }
    }
}