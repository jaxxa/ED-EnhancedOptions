using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    class PatchFireWatcher : Patch
    {

        protected override void ApplyPatch(Harmony harmony = null)
        {
            //Get the Origional CheckSpring Method
            PropertyInfo _RimWorld_FireWatcher_LargeFireDangerPresent = typeof(RimWorld.FireWatcher).GetProperty("LargeFireDangerPresent", BindingFlags.Public | BindingFlags.Instance);
            Patcher.LogNULL(_RimWorld_FireWatcher_LargeFireDangerPresent, "_RimWorld_FireWatcher_LargeFireDangerPresent");

            MethodInfo _RimWorld_FireWatcher_LargeFireDangerPresent_Getter = _RimWorld_FireWatcher_LargeFireDangerPresent.GetGetMethod();
            Patcher.LogNULL(_RimWorld_FireWatcher_LargeFireDangerPresent_Getter, "_RimWorld_FireWatcher_LargeFireDangerPresent_Getter");
            

            //Get the Prefix Patch
            MethodInfo _SupressLargeFireDangerPresentPrefix = typeof(PatchFireWatcher).GetMethod("SupressLargeFireDangerPresentPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_SupressLargeFireDangerPresentPrefix, "_SupressLargeFireDangerPresentPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_RimWorld_FireWatcher_LargeFireDangerPresent_Getter, new HarmonyMethod(_SupressLargeFireDangerPresentPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "PatchFireWatcher";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.SuppressRainFire;
        }


        // prefix
        // - wants instance, result and count
        // - wants to change count
        // - returns a boolean that controls if original is executed (true) or not (false)
        public static Boolean SupressLargeFireDangerPresentPrefix(ref bool __result)
        {

            //This is the result that will be used, note that it was passed as a ref.
            __result = false;

            //Retuen False so the origional method is not executed, overriting the false result.
            return false;
        }
    }
}
