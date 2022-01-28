namespace Input
{
    using System;
    using Signals;
    using UnityEngine;
    using Views;
    using Zenject;

    public class PlayerMovement : MonoBehaviour
    {
        [Inject]
        public SignalBus SignalBus { get; set; }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SignalBus.Fire<PlayerTryJump>();
            }
        }
    }
}