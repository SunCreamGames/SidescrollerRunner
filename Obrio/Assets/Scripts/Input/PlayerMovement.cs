namespace Input
{
    using System;
    using UnityEngine;
    using Views;

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private PlayerView _playerView;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _playerView.TryJump();
            }
        }
    }
}