namespace Views
{
    using System;
    using UnityEngine;
    using Zenject;
    using Logic;
    using Signals;

    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private Animator animator;

        private SignalBus _signalBus;

        [SerializeField]
        private float force = 50f;

        private static readonly int RunBool = Animator.StringToHash("Run");
        private static readonly int JumpBool = Animator.StringToHash("Jump");

        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<PlayerJump>(Jump);
            _signalBus.Subscribe<LevelStarted>(Run);
            _signalBus.Subscribe<LevelFailing>(Stop);
            _signalBus.Subscribe<LevelRestarting>(Reset);
        }

        private void Reset()
        {
            animator.SetBool(RunBool, false);
            animator.SetBool(JumpBool, false);
        }

        private void Awake()
        {
            Reset();
        }

        private void Stop()
        {
            animator.SetBool(RunBool, false);
        }

        private void Run()
        {
            animator.SetBool(RunBool, true);
        }


        private void Jump()
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
            animator.SetBool(JumpBool, true);
        }


        private void OnLand()
        {
            animator.SetBool(JumpBool, false);
            _signalBus.Fire<PlayerLanded>();
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            var coin = other.GetComponent<Coin>();
            if (coin != null)
            {
                _signalBus.Fire(new PickUpCoin {Coin = coin});
            }
        }


        private void OnCollisionEnter2D(Collision2D other)
        {
            var ground = other.collider.GetComponent<Ground>();
            if (ground != null)
            {
                OnLand();
                return;
            }

            var obstacle = other.collider.GetComponent<Obstacle>();
            if (obstacle != null)
            {
                _signalBus.Fire<CollisionWithObstacle>();
            }
        }
    }
}