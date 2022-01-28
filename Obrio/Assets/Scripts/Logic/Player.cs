namespace Logic
{
    using Signals;
    using Zenject;

    public class Player
    {
        private bool _canJump;

        private SignalBus _signalBus;

        [Inject]
        public Player(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _canJump = true;

            _signalBus.Subscribe<PlayerTryJump>(TryJump);
            _signalBus.Subscribe<PlayerLanded>(OnLand);
        }

        private void OnLand()
        {
            _canJump = true;
        }

        private void TryJump()
        {
            if (_canJump)
            {
                _canJump = false;
                _signalBus.Fire<PlayerJump>();
            }
        }
    }
}