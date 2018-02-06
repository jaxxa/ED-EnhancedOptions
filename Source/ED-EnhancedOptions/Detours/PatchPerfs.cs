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
    
    static class PatchPerfs
    {
        
        static public void ApplyPatches(HarmonyInstance harmony)
        {
            Log.Message("PatchDebugWindowsOpener.ApplyPatches() Starting");
            
            //Get the Origional DevMode Property
            PropertyInfo _Verse_Prefs_DevMode = typeof(Verse.Prefs).GetProperty("DevMode", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_Verse_Prefs_DevMode, "_Verse_Prefs_DevMode");
            
            //Get the Setter Method
            MethodInfo _Verse_Prefs_DevMode_Setter = _Verse_Prefs_DevMode.GetSetMethod(true);
            Patch.LogNULL(_Verse_Prefs_DevMode_Setter, "_Verse_Prefs_DevMode_Setter");
                        
            //Get the Prefix
            MethodInfo _DevModeSetterPrefix = typeof(PatchPerfs).GetMethod("DevModeSetterPrefix", BindingFlags.Public | BindingFlags.Static);
            Patch.LogNULL(_DevModeSetterPrefix, "_DevModeSetterPrefix");
            
            //Apply the Prefix Patch
            harmony.Patch(_Verse_Prefs_DevMode_Setter, new HarmonyMethod(_DevModeSetterPrefix), null);

            Log.Message("PatchDebugWindowsOpener.ApplyPatches() Completed");
        }

        public static bool DevModeSetterPrefix()
        {
            //Return False to Stop from Enabling Dev Mode.
            return false;
        }
    }
}
