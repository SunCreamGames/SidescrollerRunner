using Signals;
using Zenject;

namespace Logic
{
    public class GameSpeedController : IGameSpeedController
    {
        public float Speed { get; private set; }
        public float SpeedPoint { get; private set; }

        private SignalBus _signalBus;

        [Inject]
        public GameSpeedController(SignalBus signalBus)
        {
            // TODO : Configs
            Speed = 0f;
            SpeedPoint = 100f;
            _signalBus = signalBus;
            signalBus.Subscribe<UpdateSpeed>(UpdateSpeed);

            signalBus.Subscribe<LevelStarting>(() =>
            {
                Speed = 20f;
                signalBus.Fire<StartMoving>();
            });

            signalBus.Subscribe<LevelFailing>(() =>
            {
                Speed = 0f;
                signalBus.Fire(new SpeedUpdated {NewSpeed = 0f});
            });
        }

        private void UpdateSpeed()
        {
            Speed *= 1.05f;
            SpeedPoint *= 2.3f;
            _signalBus.Fire(new SpeedUpdated
            {
                NewSpeed = Speed
            });
        }
    }
}