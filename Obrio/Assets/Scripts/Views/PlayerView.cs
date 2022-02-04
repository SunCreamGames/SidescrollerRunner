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

        [SerializeField]
        private float force = 50f;

        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<PlayerJump>(Jump);
        }


        private void Jump()
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            PauseAnimation();
        }


        private void OnLand()
        {
            _signalBus.Fire<PlayerLanded>();
            UnpauseAnimation();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var coin = other.GetComponent<CoinView>();
            if (coin != null)
            {
                _signalBus.Fire(new PickUpCoin {Coin = coin});
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            var ground = other.collider.GetComponent<GroundView>();
            if (ground != null)
            {
                // _signalBus.Fire<CollisionWithObstacle>();
                _signalBus.Fire<PlayerLanded>();
                return;
            }

            var obstacle = other.collider.GetComponent<ObstacleView>();
            if (obstacle != null)
            {
                _signalBus.Fire<CollisionWithObstacle>();
            }
        }

        private void PauseAnimation()
        {
        }

        private void UnpauseAnimation()
        {
        }
    }
}