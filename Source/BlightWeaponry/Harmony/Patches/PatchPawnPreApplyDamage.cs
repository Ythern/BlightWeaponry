using System;
using BlightWeaponry.Defs;
using HarmonyLib;
using Verse;

namespace BlightWeaponry.Harmony.Patches
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("PreApplyDamage")]
    public class PatchPawnPreApplyDamage
    {
        [HarmonyPrefix]
        public static void Prefix(ref DamageInfo dinfo, Pawn __instance)
        {
            if (__instance?.health?.hediffSet?.hediffs.Find(hediff =>
                hediff.def.HasModExtension<BlightHediffExtension>()) is Hediff foundHediff)
            {

                bool incomingWeaponHasBlight = false;
                float damageMultiplicationFactor = 1f;
                if (dinfo.Instigator is Pawn instigatorPawn)
                {
                    Boolean? blightResult = instigatorPawn?.equipment?.Primary?.def.HasModExtension<BlightWeaponExtension>();
                    incomingWeaponHasBlight = blightResult.HasValue ? blightResult.Value : false;
                }
                if (foundHediff.Severity >= 0.25f && foundHediff.Severity < 0.75f)
                {
                    if (!incomingWeaponHasBlight)
                    {
                        damageMultiplicationFactor = 1.5f;
                    }
                }
                else if (foundHediff.Severity >= 0.75f)
                {
                    damageMultiplicationFactor = incomingWeaponHasBlight ? 1.5f : 3f;
                }
                
                dinfo.SetAmount(dinfo.Amount * damageMultiplicationFactor);
            }
        }
    }
}