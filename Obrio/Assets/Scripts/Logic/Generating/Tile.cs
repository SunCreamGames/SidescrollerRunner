namespace Logic.Generating
{
    using System.Collections.Generic;

    public class Tile
    {
        public bool HasGround { get; set; }
        public List<bool> Obstacles { get; set; }
        public List<bool> Coins { get; set; }
    }
}