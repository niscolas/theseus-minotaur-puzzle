using System.Runtime.CompilerServices;
using TheseusAndTheMinotaur;

[assembly: InternalsVisibleTo(Constants.EditorTestAssemblyName, AllInternalsVisible = true),
           InternalsVisibleTo(Constants.NSubstituteDynamicAssemblyName, AllInternalsVisible = true)]

namespace TheseusAndTheMinotaur.Theseus
{
    internal class AssemblyInfo { }
}