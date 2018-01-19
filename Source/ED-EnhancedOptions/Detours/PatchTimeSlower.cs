using Harmony;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{

    //[HarmonyPatch(typeof(Plant))]
    //[HarmonyPatch("Resting_Getter")]
    static class PatchTimeSlower
    {

        static public void ApplyPatches(HarmonyInstance harmony)
        {

            Log.Message("PatchPlant.ApplyPatches() Starting");

            //Get the Origional Method
            MethodInfo _Verse_TimeSlower_SignalForceNormalSpeed = typeof(Verse.TimeSlower).GetMethod("SignalForceNormalSpeed", BindingFlags.Public | BindingFlags.Instance);
            Patch.LogNULL(_Verse_TimeSlower_SignalForceNormalSpeed, "_Verse_TimeSlower_SignalForceNormalSpeed", true);

            //Get the Origional Method
            MethodInfo _Verse_TimeSlower_SignalForceNormalSpeedShort = typeof(Verse.TimeSlower).GetMethod("SignalForceNormalSpeedShort", BindingFlags.Public | BindingFlags.Instance);
            Patch.LogNULL(_Verse_TimeSlower_SignalForceNormalSpeedShort, "_Verse_TimeSlower_SignalForceNormalSpeedShort", true);


            //Get the Prefix Patch
            MethodInfo _PreventRunningPrefix = typeof(PatchTimeSlower).GetMethod("PreventRunningPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_PreventRunningPrefix, "_PreventRunningPrefix", true);

            //Apply the Prefix Patch
            harmony.Patch(_Verse_TimeSlower_SignalForceNormalSpeed, new HarmonyMethod(_PreventRunningPrefix), null);
            harmony.Patch(_Verse_TimeSlower_SignalForceNormalSpeedShort, new HarmonyMethod(_PreventRunningPrefix), null);

            Log.Message("PatchPlant.ApplyPatches() Completed");
        }

        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean PreventRunningPrefix()
        {
            return false;
        }

    }
}
