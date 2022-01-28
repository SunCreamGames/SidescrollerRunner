namespace Logic
{
    using Signals;
    using Zenject;

    public class ScoreCounter : IScoreCounter
    {
        private int _score;
        private SignalBus _signalBus;

        [Inject]
        public ScoreCounter(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<LevelStarting>(ResetScore);
            _signalBus.Subscribe<PickUpCoin>(() => AddScore(10));

            ResetScore();
        }

        public void AddScore(int amount)
        {
            _score += amount;
            _signalBus.Fire(new UpdateScore {Score = _score});
        }

        public void ResetScore()
        {
            _score = 0;
            _signalBus.Fire(new UpdateScore {Score = _score});
        }
    }
}