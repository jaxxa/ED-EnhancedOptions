using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using System.Reflection;
using RimWorld;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    static class PatchBuildingTurretGun
    {


        static public void ApplyPatches(HarmonyInstance harmony)
        {

            Log.Message("PatchBuildingTurretGun.ApplyPatches() Starting");

            //Get the Origional Property
            PropertyInfo _RimWorld_BuildingTurretGun_CanSetForcedTarget = typeof(RimWorld.Building_TurretGun).GetProperty("CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            Patch.LogNULL(_RimWorld_BuildingTurretGun_CanSetForcedTarget, "_RimWorld_BuildingTurretGun_CanSetForcedTarget", true);

            //Get the Property Getter Method
            MethodInfo _RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter = _RimWorld_BuildingTurretGun_CanSetForcedTarget.GetGetMethod(true);
            Patch.LogNULL(_RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter, "_RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter", false);

            //Get the Prefix Patch
            MethodInfo _CanSetForcedTargetPrefix = typeof(PatchBuildingTurretGun).GetMethod("CanSetForcedTargetPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_CanSetForcedTargetPrefix, "_CanSetForcedTargetPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter, new HarmonyMethod(_CanSetForcedTargetPrefix), null);

            Log.Message("PatchBuildingTurretGun.ApplyPatches() Completed");
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean CanSetForcedTargetPrefix(ref bool __result, ref Building_TurretGun __instance)
        {

            //Write to log to debug id the patch is running.
            //Log.Message("Prefix Running");


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
