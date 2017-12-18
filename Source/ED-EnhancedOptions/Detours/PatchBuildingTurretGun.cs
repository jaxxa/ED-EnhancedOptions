using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Harmony;
using System.Reflection;
using RimWorld;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    static class PatchBuildingTurretGun
    {


        static public void ApplyPatches(HarmonyInstance harmony)
        {
            //Get the Origional Resting Property
            PropertyInfo RimWorld_BuildingTurretGun_CanSetForcedTarget = typeof(RimWorld.Building_TurretGun).GetProperty("CanSetForcedTarget", BindingFlags.NonPublic | BindingFlags.Instance);
            Patch.LogNULL(RimWorld_BuildingTurretGun_CanSetForcedTarget, "RimWorld_BuildingTrap_CheckSpring", true);

            //Get the Resting Property Getter Method
            MethodInfo RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter = RimWorld_BuildingTurretGun_CanSetForcedTarget.GetGetMethod(true);
            Patch.LogNULL(RimWorld_BuildingTurretGun_CanSetForcedTarget, "RimWorld_BuildingTurretGun_CanSetForcedTarget", false);

            //Get the Prefix Patch
            MethodInfo prefix = typeof(PatchBuildingTurretGun).GetMethod("Prefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(prefix, "Prefix PatchBuildingTrap_Spring", true);

            //Apply the Prefix Patch
            harmony.Patch(RimWorld_BuildingTurretGun_CanSetForcedTarget_Getter, new HarmonyMethod(prefix), null);
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean Prefix(ref bool __result, ref Building_TurretGun __instance)
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
