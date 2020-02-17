using HarmonyLib;
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
    class PatchTimeSlower : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional Method
            MethodInfo _Verse_TimeSlower_SignalForceNormalSpeed = typeof(Verse.TimeSlower).GetMethod("SignalForceNormalSpeed", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_Verse_TimeSlower_SignalForceNormalSpeed, "_Verse_TimeSlower_SignalForceNormalSpeed");

            //Get the Origional Method
            MethodInfo _Verse_TimeSlower_SignalForceNormalSpeedShort = typeof(Verse.TimeSlower).GetMethod("SignalForceNormalSpeedShort", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_Verse_TimeSlower_SignalForceNormalSpeedShort, "_Verse_TimeSlower_SignalForceNormalSpeedShort");


            //Get the Prefix Patch
            MethodInfo _PreventRunningPrefix = typeof(PatchTimeSlower).GetMethod("PreventRunningPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_PreventRunningPrefix, "_PreventRunningPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_Verse_TimeSlower_SignalForceNormalSpeed, new HarmonyMethod(_PreventRunningPrefix), null);
            harmony.Patch(_Verse_TimeSlower_SignalForceNormalSpeedShort, new HarmonyMethod(_PreventRunningPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "SuppressCombatSlowdown";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.SuppressCombatSlowdown;
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
