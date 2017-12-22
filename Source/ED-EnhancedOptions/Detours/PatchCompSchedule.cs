using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    static class PatchCompSchedule
    {

        static public void ApplyPatches(HarmonyInstance harmony)
        {

            Log.Message("PatchCompSchedule.ApplyPatches() Starting");

            //Get the Origional Method
            MethodInfo _CompSchedule_RecalculateAllowed = typeof(RimWorld.CompSchedule).GetMethod("RecalculateAllowed", BindingFlags.Public | BindingFlags.Instance);
            Patch.LogNULL(_CompSchedule_RecalculateAllowed, "_CompSchedule_RecalculateAllowed", true);
            
            //Get the Prefix Patch
            MethodInfo _RecalculateAllowedPrefix = typeof(PatchCompSchedule).GetMethod("RecalculateAllowedPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_RecalculateAllowedPrefix, "_RecalculateAllowedPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_CompSchedule_RecalculateAllowed, new HarmonyMethod(_RecalculateAllowedPrefix), null);

            Log.Message("PatchCompSchedule.ApplyPatches() Completed");
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean RecalculateAllowedPrefix(ref RimWorld.CompSchedule __instance)
        {
            //Write to log to debug id the patch is running.
            Log.Message("Light Check Running");

           if (__instance.parent.def.defName == "SunLamp")
            {
                __instance.Allowed = true;
                return false; //Retuen False so the origional method is not executed
            }

            return true;
        }

    }
}
