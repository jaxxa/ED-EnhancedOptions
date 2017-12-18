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

            //Get the Origional Resting Property
            MethodInfo RimWorld_BuildingTrap_CheckSpring = typeof(RimWorld.Building_Trap).GetMethod("CheckSpring", BindingFlags.NonPublic | BindingFlags.Instance);
            Patch.LogNULL(RimWorld_BuildingTrap_CheckSpring, "RimWorld_BuildingTrap_CheckSpring", true);

            //Get the Prefix Patch
            MethodInfo prefix = typeof(PatchBuildingTrap).GetMethod("Prefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(prefix, "Prefix PatchBuildingTrap_Spring", true);

            //Apply the Prefix Patch
            harmony.Patch(RimWorld_BuildingTrap_CheckSpring, new HarmonyMethod(prefix), null);

            Log.Message("PatchBuildingTrap.ApplyPatches() Completed");
        }
        
        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean Prefix(Pawn p)
        {
            //Write to log to debug id the patch is running.
            Log.Message("Prefix Running");

            Patch.LogNULL(p, "Prefix Pawn", true);

            if (p == null) { return true; }

            if (p.Faction == null) { return true; }

            //Retuen False so the origional method is not executed.
            if (p.Faction.IsPlayer)
            {
                Log.Message("Return False");
                return false;
            }

            return true;
        }




    }
}
