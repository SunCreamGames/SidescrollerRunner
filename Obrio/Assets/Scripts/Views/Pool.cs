using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Views
{
    public class Pool : View

    {
        private int _size;
        private View _prefab;
        private Vector3 _poolRoot;

        private Queue<View> _objects;

        public View GetObject()
        {
            var res = _objects.Dequeue();
            return res;
        }

        public void Return(View o)
        {
            o.transform.position = _poolRoot;
            _objects.Enqueue(o);
        }

        public void Init(View prefab, int size, Vector3 rootPos)
        {
            _prefab = prefab;
            _poolRoot = rootPos;
            _size = size;

            _objects = new Queue<View>();
            for (int i = 0; i < _size; i++)
            {
                var a = Instantiate(_prefab);
                a.transform.position = _poolRoot;
                a.gameObject.SetActive(true);
                _objects.Enqueue(a);
            }
        }
    }
}