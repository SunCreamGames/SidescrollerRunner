namespace Views
{
    using System;
    using UnityEngine;
    using Zenject;
    using Logic;
    using Signals;

    public class PlayerView : View
    {
        [SerializeField]
        private Rigidbody2D rb;

        private SignalBus _signalBus;


        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<PlayerJump>(Jump);
        }


        private void Jump()
        {
            rb.AddForce(Vector2.up, ForceMode2D.Impulse);
            PauseAnimation();
        }


        private void OnLand()
        {
            _signalBus.Fire<PlayerLanded>();
            UnpauseAnimation();
        }


        private void OnCollisionEnter(Collision other)
        {
            var coin = other.collider.GetComponent<CoinView>();
            if (coin != null)
            {
                _signalBus.Fire<PickUpCoin>();
                return;
            }

            var obstacle = other.collider.GetComponent<ObstacleView>();
            if (obstacle != null)
            {
                _signalBus.Fire<CollisionWithObstacle>();
                return;
            }

            _signalBus.Fire<PlayerLanded>();
        }

        private void PauseAnimation()
        {
        }

        private void UnpauseAnimation()
        {
        }
    }
}