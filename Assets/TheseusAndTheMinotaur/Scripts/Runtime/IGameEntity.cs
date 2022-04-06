namespace TheseusAndTheMinotaur
{
    public interface IGameEntity
    {
        ITile CurrentTile { get; }
        void SetCurrentTile(ITile tile);
    }
}