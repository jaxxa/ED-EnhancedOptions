using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using System.Reflection;
using RimWorld;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchBuildingTurretGun : Patch
    {
        
        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional Property
            PropertyInfo _RimWorld_BuildingTurretGun_CanSetForcedTarget = typeof(RimWorld.Building_TurretGun).GetProperty("CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            Patcher.LogNULL(_RimWorld_BuildingTurretGun_CanSetForcedTarget, "_RimWorld_BuildingTurretGun_CanSetForcedTarget");

            //Get the Property Getter Method
            MethodInfo _RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter = _RimWorld_BuildingTurretGun_CanSetForcedTarget.GetGetMethod(true);
            Patcher.LogNULL(_RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter, "_RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter");

            //Get the Prefix Patch
            MethodInfo _CanSetForcedTargetPrefix = typeof(PatchBuildingTurretGun).GetMethod("CanSetForcedTargetPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_CanSetForcedTargetPrefix, "_CanSetForcedTargetPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter, new HarmonyMethod(_CanSetForcedTargetPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "CanSetForcedTargetPrefix";
        }

        protected override bool ShouldPatchApply()
        {
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
