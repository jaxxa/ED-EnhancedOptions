/*using Harmony;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace EnhancedDevelopment.EnhancedOptions.Detours
{
    
    class PatchPerfs : Patch
    {
        
        protected override void ApplyPatch(HarmonyInstance harmony = null)
        {
            //Get the Origional DevMode Property
            PropertyInfo _Verse_Prefs_DevMode = typeof(Verse.Prefs).GetProperty("DevMode", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_Verse_Prefs_DevMode, "_Verse_Prefs_DevMode");

            //Get the Setter Method
            MethodInfo _Verse_Prefs_DevMode_Setter = _Verse_Prefs_DevMode.GetSetMethod(true);
            Patcher.LogNULL(_Verse_Prefs_DevMode_Setter, "_Verse_Prefs_DevMode_Setter");

            //Get the Prefix
            MethodInfo _DevModeSetterPrefix = typeof(PatchPerfs).GetMethod("DevModeSetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patcher.LogNULL(_DevModeSetterPrefix, "_DevModeSetterPrefix");

            //Apply the Prefix Patch
            harmony.Patch(_Verse_Prefs_DevMode_Setter, new HarmonyMethod(_DevModeSetterPrefix), null);
        }

        protected override string PatchDescription()
        {
            return "LockDevMode";
        }

        protected override bool ShouldPatchApply()
        {
            return Mod_EnhancedOptions.Settings.LockDevMode;
        }

        public static bool DevModeSetterPrefix()
        {
            //Return False to Stop from Enabling Dev Mode.
            return false;
        }

    }
}
*/