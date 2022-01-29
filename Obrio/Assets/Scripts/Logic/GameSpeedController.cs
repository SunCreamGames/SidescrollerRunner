namespace Logic
{
    using Signals;
    using Zenject;

    public class GameSpeedController : IGameSpeedController
    {
        public float Speed { get; private set; }
        public float SpeedPoint { get; private set; }

        [Inject]
        public GameSpeedController(SignalBus signalBus)
        {
            // TODO : Configs
            Speed = 1f;
            SpeedPoint = 100f;
            signalBus.Subscribe<UpdateSpeed>(UpdateSpeed);
        }

        private void UpdateSpeed()
        {
            Speed *= 1.05f;
            SpeedPoint *= 2.3f;
        }
    }
}