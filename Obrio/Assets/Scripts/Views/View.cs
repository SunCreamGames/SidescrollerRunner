using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Views
{
    using Zenject;

    public class View : MonoBehaviour, IInitializable
    {
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public virtual void Initialize()
        {
        }
    }
}