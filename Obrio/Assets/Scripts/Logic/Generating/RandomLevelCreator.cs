namespace Logic.Generating
{
    using System;
    using System.Collections.Generic;

    public class RandomLevelCreator : ILevelCreator
    {
        public Tile CreateTile()
        {
            Tile tile = new Tile
            {
                ObstaclesCoordinates = new List<(float X, float Y)>(),
                CoinsCoordinates = new List<(float X, float Y)>(),
                HasGround = true
            };

            var r = new Random();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    int a;
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        a = r.Next(0, 2);
                        if (a == 1)
                        {
                            tile.ObstaclesCoordinates.Add((-6f + j * 13.5f, 4f - 2.5f * i));
                        }
                    }

                    else if (i == 2 || i % 2 != 0 && j % 2 != 0)
                    {
                        a = r.Next(0, 2);
                        if (a == 1)
                        {
                            tile.CoinsCoordinates.Add((-6f + j * 13.5f, 4f - 2.5f * i));
                        }
                    }
                }
            }

            return tile;
        }
    }
}