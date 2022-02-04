using Views;
using Zenject;

namespace Signals
{
    public class Signal
    {
    }

    public class CollisionWithObstacle : Signal
    {
    }

    public class PickUpCoin : Signal
    {
        public CoinView Coin { get; set; }
    }

    public class LevelStarting : Signal
    {
    }

    public class LevelRestarting : Signal
    {
    }

    public class LevelFailing : Signal
    {
    }

    public class PlayerTryJump : Signal
    {
    }

    public class PlayerJump : Signal
    {
    }

    public class PlayerLanded : Signal
    {
    }

    public class UpdateSpeed : Signal
    {
    }

    public class StartMoving : Signal
    {
    }

    public class SpeedUpdated : Signal
    {
        public float NewSpeed { get; set; }
    }

    public class UpdateScore : Signal
    {
        public int Score { get; set; }
    }

    public class SpawnTile : Signal
    {
        public TileView LastTile { get; set; }
    }
}