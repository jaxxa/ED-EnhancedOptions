using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    static class PatchBuildingTrap
    {

        static public void ApplyPatches(HarmonyInstance harmony)
        {

            Log.Message("PatchBuildingTrap.ApplyPatches() Starting");

            //Get the Origional CheckSpring Method
            MethodInfo _RimWorld_BuildingTrap_CheckSpring = typeof(RimWorld.Building_Trap).GetMethod("CheckSpring", BindingFlags.NonPublic | BindingFlags.Instance);
            Patch.LogNULL(_RimWorld_BuildingTrap_CheckSpring, "_RimWorld_BuildingTrap_CheckSpring", true);

            //Get the Prefix Patch
            MethodInfo _CheckSpringPrefix = typeof(PatchBuildingTrap).GetMethod("CheckSpringPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_CheckSpringPrefix, "_CheckSpringPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_BuildingTrap_CheckSpring, new HarmonyMethod(_CheckSpringPrefix), null);

            Log.Message("PatchBuildingTrap.ApplyPatches() Completed");
        }
        
        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean CheckSpringPrefix(Pawn p)
        {
            //Write to log to debug id the patch is running.
            //Log.Message("Prefix Running");

            //Patch.LogNULL(p, "Prefix Pawn", true);

            if (p == null) { return true; }

            if (p.Faction == null) { return true; }

            //Retuen False so the origional method is not executed.
            if (p.Faction.IsPlayer)
            {
                //Log.Message("Return False");
                return false;
            }

            return true;
        }
        
    }
}
