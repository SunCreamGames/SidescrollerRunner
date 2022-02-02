using System;
using System.Collections;
using UnityEngine;

namespace Views
{
    public class View : MonoBehaviour
    {
        public event Action OnSpawn;

        void Awake()
        {
            OnSpawn?.Invoke();
        }

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}