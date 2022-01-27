namespace Views
{
    using System;
    using UnityEngine;

    public class PlayerView : View
    {
        [SerializeField]
        private Rigidbody2D rb;

        private bool _canJump;

        public override void Initialize()
        {
        }

        public void TryJump()
        {
            if (_canJump)
            {
                Jump();
            }
        }

        private void Jump()
        {
            rb.AddForce(Vector2.up, ForceMode2D.Impulse);
            _canJump = false;
        }

        private void OnLand()
        {
            _canJump = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            var coin = other.collider.GetComponent<CoinView>();
            if (coin != null)
            {
                
            }
        }
    }
}