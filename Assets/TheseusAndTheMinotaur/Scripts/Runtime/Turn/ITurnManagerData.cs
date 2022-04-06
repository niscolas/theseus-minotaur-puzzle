namespace TheseusAndTheMinotaur.Turn
{
    internal interface ITurnManagerData
    {
        IMinotaurBehavior MinotaurBehavior { get; }
        ITheseusBehavior TheseusBehavior { get; }
    }
}