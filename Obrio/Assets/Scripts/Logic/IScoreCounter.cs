namespace Logic
{
    public interface IScoreCounter
    {
        void AddScore(int amount);
        void ResetScore();
    }
}