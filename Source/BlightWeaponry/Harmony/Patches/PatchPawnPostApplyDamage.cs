using System;
using BlightWeaponry.Defs;
using BlightWeaponry.Utils;
using HarmonyLib;
using Verse;

namespace BlightWeaponry.Harmony.Patches
{
    [HarmonyPatch(typeof(Pawn))]
    [HarmonyPatch("PostApplyDamage")]
    public class PatchPawnPostApplyDamage
    {
        [HarmonyPostfix]
        public static void Postfix(DamageInfo dinfo, float totalDamageDealt, Pawn __instance)
        {
            if (totalDamageDealt > 0 && dinfo.Instigator is Pawn pawnInstigator)
            {
                ThingWithComps primaryInstigatorEquipment = pawnInstigator.equipment?.Primary;
                Boolean? instigatorHasBlightWeapon =
                    primaryInstigatorEquipment?.def.HasModExtension<BlightWeaponExtension>();
                if (instigatorHasBlightWeapon.HasValue && instigatorHasBlightWeapon.Value)
                {
                    BlightWeaponExtension blightExtension =
                        primaryInstigatorEquipment?.def.GetModExtension<BlightWeaponExtension>();
                    HediffUtils.AddOrUpdateHediffWithSeverity(__instance, blightExtension.hediffToApplyOnHit, null, blightExtension.severityPerHit);
                }
            }
        }
    }
}