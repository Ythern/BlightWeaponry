namespace BlightWeaponry.Harmony
{
    public class HarmonyBase
    {
        private static HarmonyLib.Harmony harmonyInstance = new HarmonyLib.Harmony("com.blightweaponry.patch");

        public static void ApplyPatches()
        {
            harmonyInstance.PatchAll();
        }
    }
}