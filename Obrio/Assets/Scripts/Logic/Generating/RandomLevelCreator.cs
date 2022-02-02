using System;
using System.Collections.Generic;

namespace Logic.Generating
{
    public class RandomLevelCreator : ILevelCreator
    {
        public Tile CreateTile()
        {
            Tile tile = new Tile
            {
                HasGround = true,
                Obstacles = new List<bool>(),
                Coins = new List<bool>()
            };

            var r = new Random();
            for (int i = 0; i < 6; i++)
            {
                var x = r.Next(0, 10);
                tile.Obstacles.Add(x < 3);
                if (tile.Obstacles.Contains(true))
                {
                    break;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                var x = r.Next(0, 10);
                tile.Coins.Add(x < 4);
            }

            return tile;
        }

        public Tile CreateEmptyTile()
        {
            Tile tile = new Tile
            {
                HasGround = true,
                Obstacles = new List<bool>(),
                Coins = new List<bool>()
            };

            var r = new Random();
            for (int i = 0; i < 6; i++)
            {
                tile.Obstacles.Add(false);
            }

            for (int i = 0; i < 8; i++)
            {
                var x = r.Next(0, 10);
                tile.Coins.Add(false);
            }

            return tile;
        }
    }
}