using System.Runtime.CompilerServices;
using TheseusAndTheMinotaur;

[assembly: InternalsVisibleTo(Constants.EditorTestAssemblyName, AllInternalsVisible = true),
           InternalsVisibleTo(Constants.NSubstituteDynamicAssemblyName, AllInternalsVisible = true)]

namespace TheseusAndTheMinotaur.Movement
{
    internal class AssemblyInfo { }
}