using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    
    static class PatchCompBreakdownable
    {
        
        static public void ApplyPatches(HarmonyInstance harmony)
        {
            Log.Message("PatchCompBreakdownable.ApplyPatches() Starting");

            //Get the Method
            MethodInfo _CompBreakdownable_CheckForBreakdown = typeof(CompBreakdownable).GetMethod("CheckForBreakdown", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_CompBreakdownable_CheckForBreakdown, "_CompBreakdownable_CheckForBreakdown");

            //Get the Prefix
            MethodInfo _CheckForBreakdownPrefix = typeof(PatchCompBreakdownable).GetMethod("CheckForBreakdownPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_CheckForBreakdownPrefix, "_CheckForBreakdownPrefix");
            
            //Apply the Prefix Patch
            harmony.Patch(_CompBreakdownable_CheckForBreakdown, new HarmonyMethod(_CheckForBreakdownPrefix), null);

            Log.Message("PatchCompBreakdownable.ApplyPatches() Completed");
        }

        public static bool CheckForBreakdownPrefix()
        {
            //When showing forPowerOverlay return true to allow the base method to run.
            return false;
        }
    }
}
