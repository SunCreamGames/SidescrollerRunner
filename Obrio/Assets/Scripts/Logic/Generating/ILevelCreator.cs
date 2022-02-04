namespace Logic.Generating
{
    public interface ILevelCreator
    {
        public Tile CreateTile();
        public Tile CreateEmptyTile();
    }
}