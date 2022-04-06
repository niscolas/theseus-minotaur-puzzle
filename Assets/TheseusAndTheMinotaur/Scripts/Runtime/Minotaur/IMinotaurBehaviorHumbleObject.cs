namespace TheseusAndTheMinotaur.Minotaur
{
    internal interface IMinotaurBehaviorHumbleObject : IMinotaurBehaviorData
    {
        ITheseus Theseus { get; }
        ITileBasedMovement Movement { get; }
    }
}