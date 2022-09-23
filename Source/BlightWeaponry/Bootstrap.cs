using BlightWeaponry.Harmony;
using Verse;

namespace BlightWeaponry
{
    [StaticConstructorOnStartup]
    public static class Bootstrap
    {
        static Bootstrap()
        {
            HarmonyBase.ApplyPatches();
            Log.Message("[BlightWeaponry] Done intialization");
        }    
    }
}