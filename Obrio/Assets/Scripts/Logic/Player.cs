using Signals;
using Zenject;

namespace Logic
{
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
            _signalBus.Subscribe<LevelStarted>(() => 
                { _canJump = true; }
                );
            _signalBus.Subscribe<LevelFailing>(() => { _canJump = false; });
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