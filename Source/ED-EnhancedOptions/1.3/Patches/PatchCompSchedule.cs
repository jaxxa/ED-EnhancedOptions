using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    class PatchCompSchedule : Patch
    {
        
        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional Method
            MethodInfo _CompSchedule_RecalculateAllowed = typeof(RimWorld.CompSchedule).GetMethod("RecalculateAllowed", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_CompSchedule_RecalculateAllowed, "_CompSchedule_RecalculateAllowed");

            //Get the Prefix Patch
            MethodInfo _RecalculateAllowedPrefix = typeof(PatchCompSchedule).GetMethod("RecalculateAllowedPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_RecalculateAllowedPrefix, "_RecalculateAllowedPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_CompSchedule_RecalculateAllowed, new HarmonyMethod(_RecalculateAllowedPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchCompSchedule(SunLamps)";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.PlantLights24HEnabled;
        }
        
        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean RecalculateAllowedPrefix(ref RimWorld.CompSchedule __instance)
        {
            //Write to log to debug id the patch is running.

            if (__instance.parent.def.defName == "SunLamp")
            {
                __instance.Allowed = true;
                return false; //Retuen False so the origional method is not executed
            }

            return true;
        }
    }
}
