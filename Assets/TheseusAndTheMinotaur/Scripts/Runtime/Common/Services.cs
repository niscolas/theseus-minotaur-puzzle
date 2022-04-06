namespace TheseusAndTheMinotaur.Common
{
    public static class Services
    {
        public static ISpawnService SpawnService = new UnityInstantiateService();
        public static ITimeService TimeService = new UnityTimeService();
    }
}