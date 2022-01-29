namespace Logic.Generating
{
    using System.Collections.Generic;

    public class Tile
    {
        public bool HasGround { get; set; }
        public List<(float X, float Y)> ObstaclesCoordinates { get; set; }
        public List<(float X, float Y)> CoinsCoordinates { get; set; }
    }
}