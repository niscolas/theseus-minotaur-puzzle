namespace TheseusAndTheMinotaur.Theseus
{
    internal interface ITheseusBehaviorHumbleObject : ITheseusBehaviorData
    {
        ITileBasedMovement Movement { get; }
    }
}