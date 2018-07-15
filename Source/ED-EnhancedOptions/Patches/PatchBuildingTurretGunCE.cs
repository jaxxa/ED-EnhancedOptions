/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using System.Reflection;
using RimWorld;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchBuilding_TurretGunCE : Patch
    {
        
        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {
            //Get the Origional Property
            PropertyInfo _CombatExtended_BuildingTurretGunCE_CanSetForcedTarget = typeof(CombatExtended.Building_TurretGunCE).GetProperty("CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            Patcher.LogNULL(_CombatExtended_BuildingTurretGunCE_CanSetForcedTarget, "_CombatExtended_BuildingTurretGunCE_CanSetForcedTarget");

            //Get the Property Getter Method
            MethodInfo _CombatExtended_BuildingTurretGunCE_CanSetForcedTarget_Getter = _CombatExtended_BuildingTurretGunCE_CanSetForcedTarget.GetGetMethod(true);
            Patcher.LogNULL(_CombatExtended_BuildingTurretGunCE_CanSetForcedTarget_Getter, "_CombatExtended_BuildingTurretGunCE_CanSetForcedTarget_Getter");

            //Get the Prefix Patch
            MethodInfo _CanSetForcedTargetPrefix = typeof(PatchBuilding_TurretGunCE).GetMethod("CanSetForcedTargetPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_CanSetForcedTargetPrefix, "_CanSetForcedTargetPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_CombatExtended_BuildingTurretGunCE_CanSetForcedTarget_Getter, new HarmonyMethod(_CanSetForcedTargetPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "CanSetForcedTargetPrefix CombatExtended";
        }

        protected override bool ShouldPatchApply()
        {

            //Check that CombatExtended.Building_TurretGunCE exists.
            Type _CEType = Type.GetType("CombatExtended.Building_TurretGunCE, CombatExtended");
            if (_CEType == null)
            {
                Log.Message("CombatExtended.Building_TurretGunCE not found, Skipping applying Patch");
                return false;
            }

            return Mod_EnhancedOptions.Settings.TurretControlEnabled;
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean CanSetForcedTargetPrefix(ref bool __result, ref Building_TurretGun __instance)
        {

            //Allow for all Turrets belonging to the Player
            if (__instance.Faction == Faction.OfPlayer)
            {
                //Set result to true so targeting can be used and return fasle to stop origional check.
                __result = true;
                return false;
            }

            //Retuen true so the origional method is executed.
            return true;
        }
    }
}
*/