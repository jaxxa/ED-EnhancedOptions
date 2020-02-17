using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchBuildingTrap : Patch
    {
        
        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional CheckSpring Method
            MethodInfo _RimWorld_BuildingTrap_CheckSpring = typeof(RimWorld.Building_Trap).GetMethod("CheckSpring", BindingFlags.NonPublic | BindingFlags.Instance);
            Patcher.LogNULL(_RimWorld_BuildingTrap_CheckSpring, "_RimWorld_BuildingTrap_CheckSpring");

            //Get the Prefix Patch
            MethodInfo _CheckSpringPrefix = typeof(PatchBuildingTrap).GetMethod("CheckSpringPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_CheckSpringPrefix, "_CheckSpringPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_BuildingTrap_CheckSpring, new HarmonyMethod(_CheckSpringPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchBuildingTrap";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.SafeTrapEnabled;
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean CheckSpringPrefix(Pawn p)
        {

            if (p == null) { return true; }

            if (p.Faction == null) { return true; }

            //Retuen False so the origional method is not executed.

            // if (p.Faction.IsPlayer)

            //This looks to include the Player Faction as Bring Friendly to Itself.
            if (!FactionUtility.HostileTo(p.Faction, Faction.OfPlayer))
            {
                //Log.Message("Friendly");
                return false;
            }
            //Log.Message("Hostile");
            return true;
        }
    }
}
