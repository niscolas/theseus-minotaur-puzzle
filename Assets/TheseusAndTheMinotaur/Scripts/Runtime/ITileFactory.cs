namespace TheseusAndTheMinotaur
{
    public interface ITileFactory
    {
        ITile Create(int x, int y, IMap parentMap);
    }
}